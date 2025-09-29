using Client.Components;
using Client.Components.Attack;
using Client.Components.Common;
using Client.Components.Health;
using Client.Components.Movement;
using Client.Components.Weapon;
using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;

namespace Client.Systems {

    public sealed class MeleeAttackSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
                            Inc<AttackEvent, AttackerTag, AttackTarget, Position, MeleeWeapon>,
                            Exc<Inactive>> _filter;

        private readonly EcsFactoryInject<TakeDamageRequest,
                                          SourceEntity,
                                          TargetEntity,
                                          Damage> _takeDamageEmitter = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<DamageableTag> _damageableTagPool;

        public void Run(IEcsSystems systems) {
            var world = systems.GetWorld();

            var attackEventPool = _filter.Pools.Inc1;
            var attackerPool = _filter.Pools.Inc2;
            var targetPool = _filter.Pools.Inc3;
            var attackerPositionPool = _filter.Pools.Inc4;
            var meleeWeaponPool = _filter.Pools.Inc5;

            foreach (int entity in _filter.Value) {
                ref var attacker = ref attackerPool.Get(entity);
                ref var target = ref targetPool.Get(entity);
                ref var attackerPosition = ref attackerPositionPool.Get(entity);
                ref var meleeWeapon = ref meleeWeaponPool.Get(entity);

                SourceEntity sourceEntity = new SourceEntity { Value = attacker.SourceID };
                TargetEntity targetEntity = new TargetEntity { Value = target.ID };
                Damage damage = new Damage { Value = meleeWeapon.Damage };

                if (_damageableTagPool.Value.Has(target.ID)) {
                    _takeDamageEmitter.Value.NewEntity(
                        new TakeDamageRequest(),
                            sourceEntity,
                            targetEntity,
                            damage
                    );
                }

                attackEventPool.Del(entity);
            }
        }
    }
}
