using Client.Components.Attack;
using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Visual;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Systems {

    public sealed class NavMeshMovementSystem : IEcsRunSystem {
        private const float REPATH_INTERVAL = 0.3f;

        private readonly EcsFilterInject<
                            Inc<Position, Rotation, AttackTarget, NavMeshAgentComponent, AttackerTag>,
                            Exc<Inactive, Attacking>> _filter;


        private readonly EcsPoolInject<MovingTag> _movingPool;
        private readonly EcsPoolInject<NavigationRequest> _navigationPool;
        private readonly EcsPoolInject<Stopped> _stoppedPool;
        private readonly EcsPoolInject<ModelRadius> _modelRadiusPool;

        private Dictionary<int, float> _lastRepathTime = new Dictionary<int, float>();
        private readonly EcsPoolInject<RotationSpeed> _rotationSpeedPool;

        public void Run(IEcsSystems systems) {
            float currentTime = Time.time;

            foreach (int entity in _filter.Value) {
                ref var position = ref _filter.Pools.Inc1.Get(entity);
                ref var rotation = ref _filter.Pools.Inc2.Get(entity);
                ref var attackTarget = ref _filter.Pools.Inc3.Get(entity);
                ref var agentComponent = ref _filter.Pools.Inc4.Get(entity);
                ref var attackerTag = ref _filter.Pools.Inc5.Get(entity);
                var modelRadius = _modelRadiusPool.Value.Get(entity).Value;

                var agent = agentComponent.Agent;

                if (agent == null || agent.isOnNavMesh == false)
                    continue;

                if (_lastRepathTime.ContainsKey(entity) == false)
                    _lastRepathTime[entity] = 0f;

                ShowVisionArea(position.Value, attackerTag.AttackRange + modelRadius, Color.red);
                ShowVisionArea(attackTarget.LastKnownPosition, attackTarget.ModelRadius, Color.green);
 
                float desiredStoppingDistance = Mathf.Max(agent.stoppingDistance, attackerTag.AttackRange);

                //Debug.Log($"NavMeshMovementSystem:  [{entity}] DesiredStoppingDistance {desiredStoppingDistance} ({agentComponent.StoppingDistance} / {attackerTag.AttackRange})");
                //Debug.Log($"NavMeshMovementSystem:  [{entity}] Agent Remaining Distance {agent.remainingDistance}");
                //Debug.Log($"NavMeshMovementSystem:  [{entity}] Remaining Distance {Vector3.Distance(agent.transform.position, attackTarget.LastKnownPosition) - desiredStoppingDistance}");

                if (HasReachedDestination(agent, desiredStoppingDistance) == true) {

                    if (_movingPool.Value.Has(entity) == true)
                        _movingPool.Value.Del(entity);

                    if (_navigationPool.Value.Has(entity) == true)
                        _navigationPool.Value.Del(entity);

                    if (_stoppedPool.Value.Has(entity) == false)
                        _stoppedPool.Value.Add(entity);

                    agent.isStopped = true;
                }

                if (ShouldUpdatePath(entity, agent, attackTarget.LastKnownPosition, currentTime) == true) {
                    agent.stoppingDistance = desiredStoppingDistance;

                    if (agent.SetDestination(attackTarget.LastKnownPosition) == true) {
                        _lastRepathTime[entity] = currentTime;

                        if (_movingPool.Value.Has(entity) == false)
                            _movingPool.Value.Add(entity);

                        if (_navigationPool.Value.Has(entity) == false)
                            _navigationPool.Value.Add(entity);

                        if (_stoppedPool.Value.Has(entity) == true)
                            _stoppedPool.Value.Del(entity);

                        agent.isStopped = false;
                    }
                }

                if (agent.velocity.sqrMagnitude > 0.01f) {
                    position.Value = agent.transform.position;

                    if (agent.velocity.sqrMagnitude > 0.1f) {
                        var targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
                        rotation.Value = Quaternion.Slerp(rotation.Value,
                                                          targetRotation,
                                                          agent.angularSpeed * Time.deltaTime);
                    }
                }
            }
        }

        private bool ShouldUpdatePath(int entity,
                                      NavMeshAgent agent,
                                      Vector3 targetPosition, float currentTime) {

            if (agent.hasPath == false)
                return true;

            if (currentTime - _lastRepathTime[entity] < REPATH_INTERVAL)
                return false;

            float targetMoveDistance = Vector3.Distance(agent.destination, targetPosition);
            return targetMoveDistance > agent.stoppingDistance * 0.3f;
        }

        private bool HasReachedDestination(NavMeshAgent agent, float stoppingDistance) {
            float distanceToTarget = Vector3.Distance(agent.destination, agent.gameObject.transform.position);
            //Debug.Log($"NavMeshMovementSystem:  Remaining Distance {distanceToTarget}");

            if (distanceToTarget < stoppingDistance)
                return true;


            if (agent.pathPending == false && agent.remainingDistance <= stoppingDistance) {

                if (agent.hasPath == false || agent.velocity.sqrMagnitude < 0.1f)
                    return true;
            }

            return false;
        }

        private void ShowVisionArea(Vector3 center, float radius, Color color, int segments = 12) {
            float angle = 0f;
            Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            for (int i = 1; i <= segments; i++) {
                angle = i * Mathf.PI * 2f / segments;
                Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                Debug.DrawLine(lastPoint, nextPoint, color, 0.1f);
                lastPoint = nextPoint;
            }
        }
    }
}