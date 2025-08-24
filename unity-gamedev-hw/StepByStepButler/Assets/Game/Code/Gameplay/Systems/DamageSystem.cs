using UnityEngine;
using EventBusService;
using System.Collections.Generic;
using StepByStepButler.Gameplay.Heroes;

namespace StepByStepButler.Gameplay.Systems {

    public sealed class DamageSystem : IGameSystem {
        private readonly IEventBus _eventBus;
        private readonly HeroesProvider _heroes;

        private IReadOnlyList<Entity> Entities => _heroes.GetEntities();

        public DamageSystem(IEventBus eventBus,
                            HeroesProvider heroes) {

            _eventBus = eventBus;
            _heroes = heroes;
        }

        public void OnInitializeGame() {
            _eventBus.Subscribe<DamageEvent>(OnDamageEvent);
            _eventBus.Subscribe<HealEvent>(OnHealEvent);
            _eventBus.Subscribe<FreezeEvent>(OnFreezeEvent);
        }

        public void OnFinishGame() {
            _eventBus.Unsubscribe<DamageEvent>(OnDamageEvent);
            _eventBus.Unsubscribe<HealEvent>(OnHealEvent);
            _eventBus.Unsubscribe<FreezeEvent>(OnFreezeEvent);
        }

        public void OnRestartGame() { }

        private void OnDamageEvent(DamageEvent evt) {

            if (_heroes.TryGetEntityById(evt.TargetId, out var target) == false)
                return;

            var health = target.GetComponent<HealthComponent>();
            int damage = evt.Damage;

            // Divine Shield check
            if (target.HasComponent<DivineShieldComponent>() == true) {
                var shieldComp = target.GetComponent<DivineShieldComponent>();

                if (shieldComp.IsActive == true) {
                    shieldComp.IsActive = false;
                    target.RemoveComponent<DivineShieldComponent>();
                    damage = 0; // No damage taken
                }
            }

            // Apply damage
            if (damage > 0) {
                health.CurrentHealth = Mathf.Max(0, health.CurrentHealth - damage);
                target.UpdateComponent(health);

                _eventBus.Publish(new HealthChangedEvent(target.Id, health.CurrentHealth, health.MaxHealth));

                var targetType = target.GetComponent<HeroTypeComponent>().Type;

                if (targetType == HeroType.Electro && damage > 0) {
                    _eventBus.Publish(new AbilityUsedEvent(target.Id));

                    foreach (var entity in Entities) {

                        if (entity.Id != target.Id && entity.GetComponent<HealthComponent>().CurrentHealth > 0)
                            _eventBus.Publish(new DamageEvent(entity.Id, 1));
                    }
                }
            }
        }

        private void OnHealEvent(HealEvent evt) {

            if (_heroes.TryGetEntityById(evt.TargetId, out var target) == false)
                return;

            var healthComp = target.GetComponent<HealthComponent>();
            healthComp.CurrentHealth = Mathf.Min(healthComp.MaxHealth, healthComp.CurrentHealth + evt.Amount);
            target.UpdateComponent(healthComp);

            _eventBus.Publish(new HealthChangedEvent(target.Id, healthComp.CurrentHealth, healthComp.MaxHealth));
        }

        private void OnFreezeEvent(FreezeEvent evt) {

            if (_heroes.TryGetEntityById(evt.TargetId, out var target) == false)
                return;

            target.AddComponent(new FrozenComponent(1));
        }
    }
}