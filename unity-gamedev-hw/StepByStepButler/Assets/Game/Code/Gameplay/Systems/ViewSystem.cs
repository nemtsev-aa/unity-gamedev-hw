using UI;
using UnityEngine;
using EventBusService;
using Cysharp.Threading.Tasks;
using StepByStepButler.Gameplay.Systems;
using System;
using System.Linq;
using System.Collections.Generic;


namespace StepByStepButler.Gameplay.Heroes {

    public sealed class ViewSystem : IGameSystem {
        private const string HEART = "<sprite index=0>";
        private const string SWORD = "<sprite index=1>";
        private const string DEATH_TEXT = "WASTED";

        private readonly IEventBus _eventBus;
        private readonly HeroesProvider _heroes;
        private readonly TurnSystem _turnSystem;
        private readonly HeroIconsConfiguration _iconsConfig;
        private readonly UIService _uiService;
        private readonly Queue<Func<UniTask>> _animationQueue = new();
        private readonly Dictionary<int, HeroView> _heroViews = new();
        private readonly Dictionary<int, bool> _deathAnimationsInProgress = new();

        private bool _isProcessingAnimations = false;

        private IReadOnlyList<Entity> Entities => _heroes.GetEntities();
        private HeroListView RedPlayer => _uiService.GetRedPlayer();
        private HeroListView BluePlayer => _uiService.GetBluePlayer();

        public ViewSystem(
            IEventBus eventBus,
            HeroesProvider heroes,
            TurnSystem turnSystem,
            HeroIconsConfiguration iconsConfig,
            UIService uiService) {

            _eventBus = eventBus;
            _heroes = heroes;
            _turnSystem = turnSystem;
            _iconsConfig = iconsConfig;
            _uiService = uiService;
        }

        public void OnInitializeGame() {
            _eventBus.Subscribe<AttackEvent>(OnAttackEvent);
            _eventBus.Subscribe<TurnStartedEvent>(OnTurnStarted);
            _eventBus.Subscribe<HealthChangedEvent>(OnHealthChanged);
            _eventBus.Subscribe<DeathAnimationCompleteEvent>(OnDeathAnimationComplete);

            InitializeViews();
            SetupClickHandlers();
        }

        public void OnFinishGame() {
            _eventBus.Unsubscribe<AttackEvent>(OnAttackEvent);
            _eventBus.Unsubscribe<TurnStartedEvent>(OnTurnStarted);
            _eventBus.Unsubscribe<HealthChangedEvent>(OnHealthChanged);
            _eventBus.Unsubscribe<DeathAnimationCompleteEvent>(OnDeathAnimationComplete);
        }

        public void OnRestartGame() {
            OnFinishGame();

            _animationQueue.Clear();
            _heroViews.Clear();
            _deathAnimationsInProgress.Clear();
            _isProcessingAnimations = false;

            RedPlayer.OnHeroClicked -= OnHeroClicked;
            foreach (var iView in RedPlayer.GetViews()) {
                iView.gameObject.SetActive(true);
            }

            BluePlayer.OnHeroClicked -= OnHeroClicked;
            foreach (var iView in BluePlayer.GetViews()) {
                iView.gameObject.SetActive(true);
            }
        }

        private void InitializeViews() {

            foreach (var entity in Entities) {
                var playerType = entity.GetComponent<PlayerComponent>().PlayerType;
                var heroType = entity.GetComponent<HeroTypeComponent>().Type;
                var viewIndex = entity.GetComponent<ViewIndexComponent>().Index;
                var health = entity.GetComponent<HealthComponent>().CurrentHealth;
                var attack = entity.GetComponent<AttackComponent>().AttackPower;

                if (TryGetHeroViewByIndex(playerType, viewIndex, out HeroView heroView) == false)
                    return;

                var icon = GetHeroIcon(heroType);

                if (icon != null)
                    heroView.SetIcon(icon);

                heroView.SetStats($"{SWORD} {attack} / {HEART} {health}");
            }
        }

        private bool TryGetHeroViewByIndex(PlayerType type, int index, out HeroView heroView) {
            var listView = type == PlayerType.Red ? RedPlayer : BluePlayer;
            var view = listView.GetView(index);

            if (view == null) {
                heroView = default;
                return false;
            }

            heroView = view;
            return true;
        }

        private Sprite GetHeroIcon(HeroType heroType) {
            return _iconsConfig.GetIcon(heroType);
        }

        private void SetupClickHandlers() {
            RedPlayer.OnHeroClicked += OnHeroClicked;
            BluePlayer.OnHeroClicked += OnHeroClicked;
        }

        private void OnHeroClicked(HeroView heroView) {

            if (_isProcessingAnimations == true)
                return;

            var currentHero = _turnSystem.GetCurrentHero();

            if (currentHero == null)
                return;

            if (TryGetEntityByHeroView(heroView, out var targetEntity) == false)
                return;

            var targetHealth = targetEntity.GetComponent<HealthComponent>();
            var targetPlayer = targetEntity.GetComponent<PlayerComponent>().PlayerType;
            var currentPlayer = currentHero.GetComponent<PlayerComponent>().PlayerType;

            if (targetHealth.CurrentHealth <= 0 || targetPlayer == currentPlayer)
                return;

            _eventBus.Publish(new AttackEvent(currentHero.Id, targetEntity.Id));
        }

        private void OnTurnStarted(TurnStartedEvent turnEvent) {

            foreach (var entity in Entities) {

                if (TryGetHeroViewByEntity(entity, out var heroView) == true)
                    heroView.SetActive(entity.Id == turnEvent.HeroId);
            }

            if (_heroes.TryGetEntityById(turnEvent.HeroId, out var currentHero) == false)
                return;

            var activePlayerType = currentHero.GetComponent<PlayerComponent>().PlayerType;
            RedPlayer.SetActive(activePlayerType == PlayerType.Red);
            BluePlayer.SetActive(activePlayerType == PlayerType.Blue);
        }

        private void OnHealthChanged(HealthChangedEvent healthEvent) {

            if (_heroes.TryGetEntityById(healthEvent.HeroId, out var entity) == false)
                return;

            if (TryGetHeroViewByEntity(entity, out var heroView) == false)
                return;

            var heroId = healthEvent.HeroId;
            var health = healthEvent.NewHealth;
            var attack = entity.GetComponent<AttackComponent>().AttackPower;
            var type = entity.GetComponent<HeroTypeComponent>().Type;

            if (health <= 0 && heroView != null) {
                heroView.SetStats($"{DEATH_TEXT}");
                _eventBus.Publish(new HeroDeathAnimationEvent(heroId, heroView, type));
                return;
            }

            heroView.SetStats($"{SWORD} {attack} / {HEART} {health}");
        }

        private void OnAttackEvent(AttackEvent attackEvent) {

            EnqueueAnimation(async () => {
                // Получаем участников боя
                if (_heroes.TryGetEntityById(attackEvent.AttackerId, out var attacker) == false ||
                    TryGetHeroViewByEntity(attacker, out var attackerView) == false ||
                    _heroes.TryGetEntityById(attackEvent.TargetId, out var target) == false ||
                    TryGetHeroViewByEntity(target, out var targetView) == false) {
                    return;
                }

                await attackerView.AnimateAttack(targetView);
                _eventBus.Publish(new AttackAnimationCompleteEvent());

                var targetHealth = target.GetComponent<HealthComponent>();

                if (targetHealth.CurrentHealth <= 0) {
                    _deathAnimationsInProgress[target.Id] = true;

                    await UniTask.WaitUntil(() => _deathAnimationsInProgress.ContainsKey(target.Id) == false);
                }

                _eventBus.Publish(new ReadyForNextTurnEvent());
            });
        }

        private void OnDeathAnimationComplete(DeathAnimationCompleteEvent evt) {
            _deathAnimationsInProgress.Remove(evt.HeroId);
        }

        private bool TryGetEntityByHeroView(HeroView heroView, out Entity entity) {
            var redViews = RedPlayer.GetViews();
            var blueViews = BluePlayer.GetViews();

            Entity findedEntity;

            for (int i = 0; i < redViews.Count; i++) {

                if (redViews[i] == heroView) {

                    findedEntity = Entities.FirstOrDefault(e =>
                        e.GetComponent<PlayerComponent>().PlayerType == PlayerType.Red &&
                        e.GetComponent<ViewIndexComponent>().Index == i);

                    if (findedEntity != null) {
                        entity = findedEntity;
                        return true;
                    }
                }
            }

            for (int i = 0; i < blueViews.Count; i++) {

                if (blueViews[i] == heroView) {

                    findedEntity = Entities.FirstOrDefault(e =>
                        e.GetComponent<PlayerComponent>().PlayerType == PlayerType.Blue &&
                        e.GetComponent<ViewIndexComponent>().Index == i);

                    if (findedEntity != null) {
                        entity = findedEntity;
                        return true;
                    }
                }
            }

            entity = default;
            return false;
        }

        private bool TryGetHeroViewByEntity(Entity entity, out HeroView heroView) {
            var playerType = entity.GetComponent<PlayerComponent>().PlayerType;
            var viewIndex = entity.GetComponent<ViewIndexComponent>().Index;

            var listView = playerType == PlayerType.Red ? RedPlayer : BluePlayer;
            HeroView findedView = listView.GetView(viewIndex);

            if (findedView != null) {
                heroView = findedView;
                return true;
            }

            heroView = default;
            return false;
        }

        private void EnqueueAnimation(Func<UniTask> animation) {
            _animationQueue.Enqueue(animation);

            if (_isProcessingAnimations == false)
                ProcessAnimationQueue();
        }

        private async void ProcessAnimationQueue() {
            _isProcessingAnimations = true;

            while (_animationQueue.Count > 0) {
                var animation = _animationQueue.Dequeue();
                await animation();
            }

            _isProcessingAnimations = false;
        }
    }
}