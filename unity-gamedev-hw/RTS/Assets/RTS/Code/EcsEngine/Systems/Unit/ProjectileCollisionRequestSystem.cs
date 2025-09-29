using Client.Components;
using Client.Components.Common;
using Client.Components.Health;
using Client.Components.Projectile;
using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;

namespace Client.Systems {

    public sealed class ProjectileCollisionRequestSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<CollisionEnterRequest,
                                             ProjectileTag,
                                             SourceEntity,
                                             TargetEntity>> _filter = EcsWorlds.EVENTS;

        private readonly EcsFactoryInject<TakeDamageRequest,
                                          SourceEntity, 
                                          TargetEntity,
                                          Damage> _takeDamageEmitter = EcsWorlds.EVENTS;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Damage> _damagePool;
        private readonly EcsPoolInject<DeathRequest> _deathRequestPool;
        private readonly EcsPoolInject<DamageableTag> _damageableTagPool;

        public void Run(IEcsSystems systems) {
            EcsPool<SourceEntity> sourcePool = _filter.Pools.Inc3;
            EcsPool<TargetEntity> targetPool = _filter.Pools.Inc4;

            foreach (int entity in _filter.Value) {
                SourceEntity sourceEntity = sourcePool.Get(entity);
                int bullet = sourceEntity.Value;

                if (_deathRequestPool.Value.Has(bullet) == false) {
                    TargetEntity targetEntity = targetPool.Get(entity);
                    int target = targetEntity.Value;

                    if (_damageableTagPool.Value.Has(target)) {
                        _takeDamageEmitter.Value.NewEntity(
                            new TakeDamageRequest(),
                                sourceEntity,
                                targetEntity,
                                _damagePool.Value.Get(bullet)
                        );
                    }

                    //Debug.Log($"{entity}");
                    _deathRequestPool.Value.Add(bullet);
                }

                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}
