using UnityEngine;
using UnityEngine.AI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Client.Components.Attack;
using Client.Components.Common;
using Client.Components.Health;
using Client.Components.Movement;
using Client.Components.Targeting;

namespace Client.Systems {

    public sealed class AttackRequestSystem : IEcsInitSystem, IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<AttackerTag, TargetingComponent, Position, NavMeshAgentComponent, Stopped>,
            Exc<Inactive, AttackTimer, AttackRequest>> _filter;

        private readonly EcsPoolInject<TargetableComponent> _targetablePool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<AttackTimer> _attackTimerPool;
        private readonly EcsPoolInject<AttackRequest> _attackRequestPool;
        private readonly EcsPoolInject<Attacking> _attackingPool;
        private readonly EcsPoolInject<Stopped> _stoppedPool;
        private EcsWorld _world;

        public void Init(IEcsSystems systems) {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems) {

            foreach (int entity in _filter.Value) {
                ref var attacker = ref _filter.Pools.Inc1.Get(entity);
                ref var targeting = ref _filter.Pools.Inc2.Get(entity);
                ref var position = ref _filter.Pools.Inc3.Get(entity);
                ref var agent = ref _filter.Pools.Inc4.Get(entity).Agent;

                if (IsCurrentTargetValid(entity, ref targeting) == false ||
                    CheckDistanceToTarget(agent, attacker) == false) {

                    StopAttacking(entity);

                    if (agent.isOnNavMesh == true)
                        agent.isStopped = false;

                    continue;
                }

                StartAttacking(entity, attacker);
            }
        }

        private bool IsCurrentTargetValid(int entity, ref TargetingComponent targeting) {

            if (targeting.CurrentTargetId == -1)
                return false;

            if (_world.IsEntityAlive(targeting.CurrentTargetId) == false)
                return false;

            if (_targetablePool.Value.Has(targeting.CurrentTargetId) == false ||
                _targetablePool.Value.Get(targeting.CurrentTargetId).IsActive == false)
                return false;

            if (_healthPool.Value.Has(targeting.CurrentTargetId) == true &&
                _healthPool.Value.Get(targeting.CurrentTargetId).Value <= 0)
                return false;

            return true;
        }

        private bool CheckDistanceToTarget(NavMeshAgent agent, AttackerTag attackerTag) {

            if (agent == null)
                return false;

            var distanceToTarget = Vector3.Distance(agent.transform.position, agent.destination) - agent.stoppingDistance;
            //Debug.Log($"AttackRequestSystem: [{attackerTag.SourceID}] Distance To Target {distanceToTarget} / AttackRange {attackerTag.AttackRange}");

            if (distanceToTarget > attackerTag.AttackRange)
                return false;

            return true;
        }

        private void StartAttacking(int attackerEntity, AttackerTag attacker) {

            if (_attackingPool.Value.Has(attackerEntity) == false)
                _attackingPool.Value.Add(attackerEntity);

            _attackRequestPool.Value.Add(attackerEntity);

            // Установка таймера атаки
            _attackTimerPool.Value.Add(attackerEntity) = new AttackTimer {
                Value = attacker.AttackRate
            };
        }

        private void StopAttacking(int entity) {

            if (_attackingPool.Value.Has(entity) == true)
                _attackingPool.Value.Del(entity);

            if (_stoppedPool.Value.Has(entity) == true)
                _stoppedPool.Value.Del(entity);
        }
    }
}
