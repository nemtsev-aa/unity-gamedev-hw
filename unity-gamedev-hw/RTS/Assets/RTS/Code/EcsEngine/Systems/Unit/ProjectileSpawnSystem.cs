using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Projectile;
using Client.Components.Weapon;
using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Client.Systems {

    public sealed class ProjectileSpawnSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<ProjectileSpawnRequest>> _filter = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<SpawnRequest> _spawnPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;

        public void Run(IEcsSystems systems) {
            var world = systems.GetWorld();
            var requestPool = _filter.Pools.Inc1;

            foreach (int entity in _filter.Value) {
                ref var iData = ref requestPool.Get(entity);

                int spawnEvent = _eventWorld.Value.NewEntity();
                Entity shooter = iData.OwnerEntity;

                _spawnPool.Value.Add(spawnEvent) = new SpawnRequest();

                _positionPool.Value.Add(spawnEvent) = new Position {
                    Value = shooter.GetData<RangeWeapon>().FirePoint.position
                };

                _rotationPool.Value.Add(spawnEvent) = new Rotation {
                    Value = shooter.GetData<Rotation>().Value
                };

                _prefabPool.Value.Add(spawnEvent) = new Prefab {
                    Value = shooter.GetData<RangeWeapon>().ProjectalePrefab
                };

                requestPool.Del(entity);

                _positionPool.Value.Del(entity);
                _rotationPool.Value.Del(entity);
                _prefabPool.Value.Del(entity);

                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}
