using UnityEngine;
using System.Collections.Generic;
using StepByStepButler.Gameplay;
using StepByStepButler.Gameplay.Heroes;
using Object = UnityEngine.Object;

namespace UI.Components.DamagePopupSystem {

    public sealed class DamagePopupPool {
        private const int DEFAULT_POOL_SIZE = 10;

        private readonly DamagePopupView _prefab;
        private readonly Transform _parent;
        private readonly Queue<DamagePopupView> _pool = new();
        private readonly List<DamagePopupView> _activePopups = new();
        private readonly HeroesProvider _heroes;
        private readonly UIService _uiService;

        private readonly Dictionary<int, DamagePopupView> _activePopupsByHero = new();

        private HeroListView RedPlayer => _uiService.GetRedPlayer();
        private HeroListView BluePlayer => _uiService.GetBluePlayer();

        public DamagePopupPool(DamagePopupView prefab,
                               Transform parent,
                               HeroesProvider heroes,
                               UIService uiService) {
            _prefab = prefab;
            _parent = parent;
            _heroes = heroes;
            _uiService = uiService;

            Prewarm(DEFAULT_POOL_SIZE);
        }

        private void Prewarm(int count) {
            for (int i = 0; i < count; i++) {
                var popup = CreatePopup();
                _pool.Enqueue(popup);
            }
        }

        public void ShowDamage(int amount, int heroId) {
            if (TryGetHeroView(heroId, out var heroView)) {

                if (_activePopupsByHero.TryGetValue(heroId, out var existingPopup)) {
                    existingPopup.ChangeTextValue(amount, false);
                } else {
                    var popup = GetPopup();
                    _activePopupsByHero[heroId] = popup;
                    popup.ShowDamage(amount, heroView, (completedPopup) => {
                        _activePopupsByHero.Remove(heroId);
                        ReturnPopup(completedPopup);
                    });
                }
            }
        }

        public void ShowHeal(int amount, int heroId) {
            if (TryGetHeroView(heroId, out var heroView)) {

                if (_activePopupsByHero.TryGetValue(heroId, out var existingPopup)) {
                    existingPopup.ChangeTextValue(amount, true);
                } else {
                    var popup = GetPopup();
                    _activePopupsByHero[heroId] = popup;
                    popup.ShowHeal(amount, heroView, (completedPopup) => {
                        _activePopupsByHero.Remove(heroId);
                        ReturnPopup(completedPopup);
                    });
                }
            }
        }

        private DamagePopupView GetPopup() {
            DamagePopupView popup;

            if (_pool.Count > 0) {
                popup = _pool.Dequeue();
            } else {
                popup = CreatePopup();
                Debug.Log("Pool exhausted, creating new popup. Consider increasing pool size.");
            }

            popup.gameObject.SetActive(true);
            _activePopups.Add(popup);

            return popup;
        }

        public void ReturnPopup(DamagePopupView popup) {

            if (popup == null)
                return;

            popup.Reset();
            popup.gameObject.SetActive(false);

            if (_activePopups.Remove(popup) == true)
                _pool.Enqueue(popup);
        }

        private bool TryGetHeroView(int heroId, out HeroView heroView) {
            heroView = null;

            if (_heroes.TryGetEntityById(heroId, out Entity entity) &&
                TryGetHeroViewByEntity(entity, out heroView)) {
                return true;
            }

            return false;
        }

        public void Reset() {
            _activePopupsByHero.Clear();

            foreach (var popup in _activePopups) {
                popup.Reset();
                popup.gameObject.SetActive(false);
                _pool.Enqueue(popup);
            }

            _activePopups.Clear();
        }

        private DamagePopupView CreatePopup() {
            var popup = Object.Instantiate(_prefab, _parent);
            popup.gameObject.SetActive(false);
            return popup;
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
    }
}
