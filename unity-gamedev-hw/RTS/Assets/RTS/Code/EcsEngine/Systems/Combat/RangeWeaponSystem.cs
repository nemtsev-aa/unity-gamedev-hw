using Leopotam.EcsLite.Di;

namespace Client.Systems {
    using Client.Components.Attack;
    using Client.Components.Common;
    using Client.Components.Movement;
    using Client.Components.Projectile;
    using Client.Components.Weapon;
    using Client.Services;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.Entities;
    using UnityEngine;

    public sealed class RangeWeaponSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<AttackEvent, AttackTarget, RangeWeapon>,
            Exc<Inactive>> _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<ProjectileSpawnRequest> _projectileSpawnPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<ProjectileInitRequest> _projectileInitPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS;

        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems) {
            var world = systems.GetWorld();
            var attackEventPool = _filter.Pools.Inc1;

            foreach (int entity in _filter.Value) {
                ref var target = ref _filter.Pools.Inc2.Get(entity);
                ref var weapon = ref _filter.Pools.Inc3.Get(entity);

                // 1. Создание снаряда
                CreateProjectile(entity, ref target, ref weapon);

                // 2. Удаление Event'a
                attackEventPool.Del(entity);
            }
        }

        private void CreateProjectile(int attackerEntity, ref AttackTarget target, ref RangeWeapon rangeWeapon) {
            Vector3 attackDirection = (target.LastKnownPosition - rangeWeapon.FirePoint.position).normalized;
            Quaternion attackRotation = Quaternion.LookRotation(attackDirection);

            int projectileSpawnEvent = _eventWorld.Value.NewEntity();
            _projectileSpawnPool.Value.Add(projectileSpawnEvent) = new ProjectileSpawnRequest {
                OwnerEntity = _entityManager.Value.Get(attackerEntity)
            };

            _positionPool.Value.Add(projectileSpawnEvent) = new Position {
                Value = rangeWeapon.FirePoint.position
            };

            _rotationPool.Value.Add(projectileSpawnEvent) = new Rotation {
                Value = attackRotation
            };

            _prefabPool.Value.Add(projectileSpawnEvent) = new Prefab {
                Value = rangeWeapon.ProjectalePrefab
            };

            int projectileInitEvent = _eventWorld.Value.NewEntity();
            _projectileInitPool.Value.Add(projectileInitEvent) = new ProjectileInitRequest {
                InitData = new ProjectileData {
                    Speed = rangeWeapon.ProjectaleSpeed,
                    Damage = rangeWeapon.ProjectaleDamage,
                    OwnerEntity = _entityManager.Value.Get(attackerEntity),
                    TargetEntity = _entityManager.Value.Get(target.ID)
                }
            };
        }
    }
}
