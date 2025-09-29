using Client.Components;
using Client.Components.Health;
using Client.Components.Movement;
using Client.Components.Projectile;
using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems {

    public sealed class ProjectileInitSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
                            Inc<ProjectileInitRequest>> _initFilter = EcsWorlds.EVENTS;

        private readonly EcsFilterInject<
                            Inc<ProjectileInitTag>,
                            Exc<ProjectileTag>> _unInitFilter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems) {

            int requestCount = _initFilter.Value.GetEntitiesCount();
            int unInitProjectileCount = _unInitFilter.Value.GetEntitiesCount();

            if (requestCount == 0 || unInitProjectileCount == 0)
                return;

            var initRequestPool = _initFilter.Pools.Inc1;
            var projectileInitTagPool = _unInitFilter.Pools.Inc1;

            foreach (var iInitRequest in _initFilter.Value) {
                ref var iData = ref initRequestPool.Get(iInitRequest).InitData;

                foreach (int iEntity in _unInitFilter.Value) {
                    ref var entity = ref projectileInitTagPool.Get(iEntity);

                    var viewEntity = _entityManager.Value.Get(entity.ViewEntity.Id);
                    viewEntity.name = $"Projectile [{viewEntity.Id}]";

                    Vector3 direction = (iData.TargetEntity.transform.position + Vector3.up * 0.5f) - entity.ViewEntity.transform.position;

                    viewEntity.AddData(new ProjectileData {
                        Damage = iData.Damage,
                        Speed = iData.Speed,
                        OwnerEntity = iData.OwnerEntity,
                        TargetEntity = iData.TargetEntity
                    });

                    viewEntity.AddData(new MoveDirection { Value = direction });
                    viewEntity.AddData(new MoveSpeed { Value = iData.Speed });
                    viewEntity.AddData(new Damage { Value = iData.Damage });
                    viewEntity.AddData(new ProjectileTag());

                    //Debug.Log($"ProjectileSystem: Install!");

                    projectileInitTagPool.Del(iEntity);
                    initRequestPool.Del(iInitRequest);
                }
            }
        }
    }
}
