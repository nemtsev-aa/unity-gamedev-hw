using Client.Components;
using Client.Components.Common;
using Client.Components.Movement;
using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Systems {

    public sealed class SpawnEventSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<SpawnRequest, Position, Rotation, Prefab>> _filter = EcsWorlds.EVENTS;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems) {

            var spawnRequestPool = _filter.Pools.Inc1;
          
            foreach (int @event in _filter.Value) {
                Vector3 position = _filter.Pools.Inc2.Get(@event).Value;
                Quaternion rotation = _filter.Pools.Inc3.Get(@event).Value;
                Entity prefab = _filter.Pools.Inc4.Get(@event).Value;

                _entityManager.Value.Create(prefab, position, rotation);

                spawnRequestPool.Del(@event);
                _eventWorld.Value.DelEntity(@event);

                //Debug.Log($"SpawnEventSystem: Creation complited!");
            }
        }
    }
}
