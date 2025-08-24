using EventBusService;
using System.Linq;
using System.Collections.Generic;
using StepByStepButler.Gameplay.Heroes;
using Random = UnityEngine.Random;

namespace StepByStepButler.Gameplay.Systems {

    public sealed class CombatSystem : IGameSystem {
        public const int DEVOURER_FINISH_ATTACK_DAMAGE = 3;
        public const float STUPID_ORC_MISS_CHANCE = 0.5f;
        public const float LORD_VAMP_HEALTH_STEALING_CHANCE = 0.5f;
        public const int MEDIATOR_HEALING_COUNT = 1;

        private readonly IEventBus _eventBus;
        private readonly HeroesProvider _heroesSystem;
        private PlayerType _losingTeam;

        private IReadOnlyList<Entity> Entities => _heroesSystem.GetEntities();


        public CombatSystem(IEventBus eventBus, HeroesProvider heroSystem) {
            _eventBus = eventBus;
            _heroesSystem = heroSystem;
        }

        public void OnInitializeGame() {
            _eventBus.Subscribe<AttackEvent>(OnAttack);
            _eventBus.Subscribe<DeathAnimationCompleteEvent>(OnDeathAnimationComplete);
        }

        public void OnFinishGame() {
            _eventBus.Unsubscribe<AttackEvent>(OnAttack);
            _eventBus.Unsubscribe<DeathAnimationCompleteEvent>(OnDeathAnimationComplete);
        }

        public void OnRestartGame() { }

        private void OnAttack(AttackEvent evt) {

            if (_heroesSystem.TryGetEntityById(evt.AttackerId, out var attacker) == false ||
                _heroesSystem.TryGetEntityById(evt.TargetId, out var target) == false ||
                IsValidAttack(attacker, target) == false)
                return;

            var finalTarget = HandleStupidOrcAbility(attacker, target);
            var damage = attacker.GetAttackPower();

            _eventBus.Publish(new DamageEvent(finalTarget.Id, damage, attacker.Id));

            HandleDevourerAbility(attacker);
            HandleCounterAttack(attacker, finalTarget);
            HandleLordVampAbility(attacker, damage);
            HandleIceMageAbility(attacker, finalTarget);
            HandleMeditatorAbility(attacker);
        }

        private bool IsValidAttack(Entity attacker, Entity target) {
            return attacker != null && target != null &&
                   attacker.IsAlive() == true && target.IsAlive() == true &&
                   attacker.GetTeam() != target.GetTeam();
        }

        private Entity HandleStupidOrcAbility(Entity attacker, Entity originalTarget) {

            if (attacker.GetHeroType() == HeroType.StupidOrc && Random.value < STUPID_ORC_MISS_CHANCE) {
                var otherEnemies = Entities
                    .Where(e => (e.GetTeam() != attacker.GetTeam()) &&
                                 e.Id != originalTarget.Id &&
                                 e.IsAlive() == true)
                    .ToList();

                return otherEnemies.Count() > 0 ?
                    otherEnemies[Random.Range(0, otherEnemies.Count())] :
                    originalTarget;
            }
            return originalTarget;
        }

        private void HandleCounterAttack(Entity attacker, Entity target) {

            if (attacker.GetHeroType() != HeroType.Huntress &&
                target.IsAlive() == true)

                _eventBus.Publish(new DamageEvent(attacker.Id, target.GetAttackPower(), target.Id));
        }

        private void HandleDevourerAbility(Entity attacker) {

            if (attacker.GetHeroType() != HeroType.Devourer)
                return;

            var enemies = Entities
                .Where(e => e.GetTeam() != attacker.GetTeam() &&
                            e.IsAlive() == true)
                .ToList();

            if (enemies.Count() > 0) {
                var target = enemies[Random.Range(0, enemies.Count())];
                _eventBus.Publish(new DamageEvent(target.Id, DEVOURER_FINISH_ATTACK_DAMAGE, attacker.Id));
            }
        }

        private void HandleLordVampAbility(Entity attacker, int damage) {

            if (attacker.GetHeroType() == HeroType.LordVamp &&
                Random.value < LORD_VAMP_HEALTH_STEALING_CHANCE)

                _eventBus.Publish(new HealEvent(attacker.Id, damage));
        }

        private void HandleIceMageAbility(Entity attacker, Entity target) {

            if (attacker.GetHeroType() == HeroType.IceMage)
                _eventBus.Publish(new FreezeEvent(target.Id));
        }

        private void HandleMeditatorAbility(Entity attacker) {
            
            if (attacker.GetHeroType() != HeroType.Meditator)
                return;

            var allies = Entities
                 .Where(e => (e.GetTeam() == attacker.GetTeam()) && 
                              e.IsAlive() == true)
                 .ToList();

            if (allies.Count() > 0) {
                var target = allies[Random.Range(0, allies.Count())];
                _eventBus.Publish(new HealEvent(target.Id, MEDIATOR_HEALING_COUNT));
            }
        }

        private void OnDeathAnimationComplete(DeathAnimationCompleteEvent @event) {

            if (_heroesSystem.TryGetEntityById(@event.HeroId, out var deadHero) == false)
                return;

            var team = deadHero.GetTeam();
            var aliveTeamMembers = Entities.Count(e => e.GetTeam() == team &&
                                                  e.IsAlive() == true);

            if (aliveTeamMembers == 0) {
                _losingTeam = team == PlayerType.Red ? 
                    PlayerType.Blue : 
                    PlayerType.Red;

                _eventBus.Publish(new GameOverEvent(_losingTeam));
            }
        }
    }
}