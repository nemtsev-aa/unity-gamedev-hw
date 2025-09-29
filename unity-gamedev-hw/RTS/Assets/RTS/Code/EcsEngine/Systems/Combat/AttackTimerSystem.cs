using Client.Components.Attack;
using Client.Components.Common;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {
    public sealed class AttackTimerSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<AttackTimer>,
            Exc<Inactive>> _filter;

        public void Run(IEcsSystems systems) {

            foreach (int timer in _filter.Value) {
                ref var timerValue = ref _filter.Pools.Inc1.Get(timer).Value;

                timerValue -= Time.deltaTime;
                //Debug.Log($"AttackTimerSystem: Run ID [{timer}] Value [{timerValue}]");

                if (timerValue <= 0)
                    _filter.Pools.Inc1.Del(timer);
            }
        }
    }
}

