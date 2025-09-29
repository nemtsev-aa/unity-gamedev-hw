using Client.Components;
using Client.Components.Common;
using Client.Components.Health;
using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {
    public sealed class TakeDamageRequestSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<TakeDamageRequest, TargetEntity, Damage>,
            Exc<Inactive>> _filter = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<TakeDamageEvent> _eventPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<OneFrame> _oneFramePool = EcsWorlds.EVENTS;

        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<Health> _healthPool;

        public void Run(IEcsSystems systems) {

            foreach (int @event in _filter.Value) {
                int target = _filter.Pools.Inc2.Get(@event).Value;
                int damage = _filter.Pools.Inc3.Get(@event).Value;

                if (_world.Value.IsEntityAlive(target) && _healthPool.Value.Has(target)) {
                    ref int health = ref _healthPool.Value.Get(target).Value;
                    health = Mathf.Max(0, health - damage);

                    if (health > 0) {
                        _eventPool.Value.Add(@event) = new TakeDamageEvent();
                        _oneFramePool.Value.Add(@event) = new OneFrame();
                    }
                }

                _filter.Pools.Inc1.Del(@event);
            }
        }
    }
}
