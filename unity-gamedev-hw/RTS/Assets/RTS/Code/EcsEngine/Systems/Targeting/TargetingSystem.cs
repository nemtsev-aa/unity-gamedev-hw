using Client.Components;
using Client.Components.Attack;
using Client.Components.Common;
using Client.Components.Health;
using Client.Components.Movement;
using Client.Components.Targeting;
using Client.Components.Teams;
using Client.Components.Visual;
using Client.Systems.Spatial;
using Code.OOP;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;

using UnityEngine;

namespace Client.Systems {

    public sealed class TargetingSystem : IEcsInitSystem, IEcsRunSystem {
        private const float TARGET_UPDATE_INTERVAL = 0.01f;

        private readonly EcsFilterInject<
            Inc<AttackerTag, Position, Team, TargetingComponent, VisionComponent>,
            Exc<Inactive>> _attackerFilter;

        private readonly EcsFilterInject<
            Inc<Position, Team, Health, ModelRadius, TargetableComponent>,
            Exc<Inactive>> _targetsFilter;

        private readonly EcsPoolInject<AttackerTag> _attackerPool;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<TargetingComponent> _targetingPool;
        private readonly EcsPoolInject<VisionComponent> _visionPool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<ModelRadius> _modelRadiusPool;
        private readonly EcsPoolInject<TargetableComponent> _targetablePool;
        private readonly EcsPoolInject<AttackTarget> _attackTargetPool;
        private readonly EcsPoolInject<Rotation> _rotationPool;
        private readonly EcsPoolInject<Attacking> _attackingPool;

        private readonly EcsCustomInject<SpatialTargetingGridSystem> _spatialGrid;
        private readonly EcsCustomInject<SphereCastTargetingSystem> _sphereCastSystem;

        private EcsWorld _world;
        private LayerMask _targetLayers = LayerMask.GetMask("Units", "Buildings");

        private Dictionary<int, float> _lastTargetUpdateTime = new Dictionary<int, float>();
        private Dictionary<int, int> _currentTargets = new Dictionary<int, int>();

        private TargetCandidate _blueBase;
        private TargetCandidate _redBase;

        public void Init(IEcsSystems systems) {
            _world = systems.GetWorld();

            CreateDefaultCandidates();

            // Проверяем инициализацию зависимостей
            if (_spatialGrid.Value == null) {
                Debug.LogError("SpatialTargetingGridSystem not initialized!");
            }
        }

        public void Run(IEcsSystems systems) {
            // Проверяем доступность spatial grid
            if (_spatialGrid.Value == null) {
                Debug.LogWarning("SpatialTargetingGridSystem is not available");
                return;
            }

            var currentTime = Time.time;

            foreach (int attackerEntity in _attackerFilter.Value) {
                TargetingProcess(attackerEntity, currentTime);
            }
        }

        private void TargetingProcess(int attackerEntity, float currentTime) {

            if (_lastTargetUpdateTime.ContainsKey(attackerEntity) == false)
                _lastTargetUpdateTime[attackerEntity] = 0f;

            if (currentTime - _lastTargetUpdateTime[attackerEntity] < TARGET_UPDATE_INTERVAL)
                return;

            if (HasRequiredComponents(attackerEntity) == false)
                return;

            ref var attacker = ref _attackerPool.Value.Get(attackerEntity);
            ref var attackerPos = ref _positionPool.Value.Get(attackerEntity);
            var attackerTeam = _teamPool.Value.Get(attackerEntity);
            ref var targeting = ref _targetingPool.Value.Get(attackerEntity);
            var vision = _visionPool.Value.Get(attackerEntity);

            if (IsCurrentTargetValid(attackerEntity, ref targeting) == false) {
                
                if (_attackingPool.Value.Has(attackerEntity) == true)
                    _attackingPool.Value.Del(attackerEntity);

                // Ищем новую цель с проверкой на null
                var firstTarget = FindBestTarget(attackerEntity, attackerPos.Value, attackerTeam, vision);
                UpdateTargetingComponent(attackerEntity, ref targeting, firstTarget, currentTime);
                UpdateCurrentTargetPosition(attackerEntity, ref targeting);

                _lastTargetUpdateTime[attackerEntity] = currentTime;
                return;
            }

            if (CheckEnemyBaseAttackPriority(attackerPos.Value, targeting) == true)
                return;

            ShowVisionArea(attackerPos.Value, vision.VisionRange, Color.white);
            
            var newTarget = FindBestTarget(attackerEntity, attackerPos.Value, attackerTeam, vision);

            if (newTarget != null && newTarget.Value.EntityId != targeting.CurrentTargetId) {
                var currentScore = CalculateTargetScore(attackerEntity, targeting.CurrentTargetId, attackerPos.Value);
                var newScore = newTarget.Value.Score;

                if (newScore > currentScore)
                    UpdateTargetingComponent(attackerEntity, ref targeting, newTarget, currentTime);

                if (currentTime - targeting.LastTargetUpdateTime < TARGET_UPDATE_INTERVAL)
                    UpdateCurrentTargetPosition(attackerEntity, ref targeting);

                return;
            }
        }

        // Оптимизированная проверка компонентов
        private bool HasRequiredComponents(int entity) {
            return _positionPool.Value.Has(entity) &&
                   _teamPool.Value.Has(entity) &&
                   _targetablePool.Value.Has(entity) &&
                   _healthPool.Value.Has(entity) &&
                   _modelRadiusPool.Value.Has(entity);
        }

        private void CreateDefaultCandidates() {
            var enemyBases = _world.Filter<Team>()
                    .Inc<UnitSpawner>()
                    .Exc<Inactive>()
                    .End();

            foreach (int baseEntity in enemyBases) {
                
                if (_teamPool.Value.Get(baseEntity).Value == TeamTypes.Blue) {
                    _blueBase = new TargetCandidate {
                        EntityId = baseEntity,
                        Score = float.MinValue,
                        Position = _positionPool.Value.Get(baseEntity).Value
                    };
                }

                if (_teamPool.Value.Get(baseEntity).Value == TeamTypes.Red) {
                    _redBase = new TargetCandidate {
                        EntityId = baseEntity,
                        Score = float.MinValue,
                        Position = _positionPool.Value.Get(baseEntity).Value
                    };
                }
            }
        }

        private bool IsCurrentTargetValid(int attackerEntity, ref TargetingComponent targeting) {
            if (targeting.CurrentTargetId == -1)
                return false;

            if (!_world.IsEntityAlive(targeting.CurrentTargetId))
                return false;

            if (!_targetablePool.Value.Has(targeting.CurrentTargetId) ||
                !_targetablePool.Value.Get(targeting.CurrentTargetId).IsActive)
                return false;

            if (_healthPool.Value.Has(targeting.CurrentTargetId) &&
                _healthPool.Value.Get(targeting.CurrentTargetId).Value <= 0)
                return false;

            return true;
        }

        private bool CheckEnemyBaseAttackPriority(Vector3 attackerPosition,
                                                  TargetingComponent targeting) {

            var distance = Vector3.Distance(attackerPosition, targeting.LastKnownTargetPosition);
            return distance <= 10f;
        }

        private void UpdateCurrentTargetPosition(int attackerEntity, ref TargetingComponent targeting) {
            
            if (targeting.CurrentTargetId == -1)
                return;

            if (_positionPool.Value.Has(targeting.CurrentTargetId)) {
                targeting.LastKnownTargetPosition = _positionPool.Value.Get(targeting.CurrentTargetId).Value;

                // Обновляем AttackTarget компонент
                UpdateAttackTargetComponent(attackerEntity, targeting.CurrentTargetId);
            }
        }

        private TargetCandidate? FindBestTarget(int attackerEntity, Vector3 attackerPosition,
                                                Team attackerTeam, VisionComponent vision) {

            if (_sphereCastSystem.Value == null)
                return null;

            try {
                float searchRadius = vision.VisionRange;

                // Получаем цели через сферкаст, отсортированные по расстоянию
                var potentialTargets = _sphereCastSystem.Value.GetPotentialTargetsInRange(
                    attackerPosition,
                    searchRadius,
                    attackerTeam,
                    _targetLayers,
                    true // Сортировать по расстоянию
                );

                if (potentialTargets == null || potentialTargets.Count == 0)
                    return null;

                TargetCandidate? bestCandidate = null;
                float bestScore = float.MinValue;

                // Проходим от ближайших к дальним, можем раньше выйти из цикла
                foreach (var candidate in potentialTargets) {

                    if (IsTargetVisible(attackerPosition, candidate.EntityId, vision) == false)
                        continue;

                    var score = CalculateTargetScore(attackerEntity, candidate.EntityId, attackerPosition);

                    candidate.SetScore(score);

                    // Ранний выход если нашли очень хорошую цель
                    if (score > 500f) {
                        bestCandidate = candidate;
                        break;
                    }

                    if (score > bestScore) {
                        bestScore = score;
                        bestCandidate = candidate;
                    }

                    // Если цель слишком далекая, пропускаем остальные
                    if (candidate.Distance > vision.VisionRange * 0.8f)
                        break;
                }

                return bestCandidate;
            }
            catch (System.Exception ex) {
                Debug.LogError($"Error in FindBestTarget: {ex.Message}");
                return null;
            }
        }

        private bool IsTargetVisible(Vector3 attackerPosition, int targetEntity, VisionComponent vision) {
            if (!_positionPool.Value.Has(targetEntity))
                return false;

            var targetPosition = _positionPool.Value.Get(targetEntity).Value;
            var direction = (targetPosition - attackerPosition);
            float distance = direction.magnitude;

            if (distance > vision.VisionRange)
                return false;

            direction.Normalize();

            if (vision.VisionAngle < 360f && _rotationPool.Value.Has(targetEntity)) {
                var rotation = _rotationPool.Value.Get(targetEntity).Value;
                var forward = rotation * Vector3.forward;
                var angleCos = Vector3.Dot(forward, direction);

                if (angleCos < Mathf.Cos(vision.VisionAngle * Mathf.Deg2Rad / 2f))
                    return false;
            }

            RaycastHit hitInfo;
            if (Physics.Raycast(attackerPosition, direction, out hitInfo,
                distance, vision.VisionBlockingLayers)) {
                // Проверяем, не попали ли мы в саму цель
                var hitEntityProxy = hitInfo.collider.GetComponent<EntityProxy>();

                if (hitEntityProxy == null || hitEntityProxy.Entity.Id != targetEntity)
                    return false;
            }

            return true;
        }

        private float CalculateTargetScore(int attackerEntity, int targetEntity, Vector3 attackerPosition) {
            float score = 0f;

            var targetPos = _positionPool.Value.Get(targetEntity).Value;
            var distance = Vector3.Distance(attackerPosition, targetPos);
            var targetable = _targetablePool.Value.Get(targetEntity);
            var health = _healthPool.Value.Get(targetEntity);

            var distancePoints = (1f - Mathf.Clamp01(distance / 50f)) * 30f;
            var targetTypePriorityPoints = GetTargetTypePriority(targetable.Type) * 10f;

            var constTargetPoints = 0f;
            if (_targetingPool.Value.Has(attackerEntity) &&
                _targetingPool.Value.Get(attackerEntity).CurrentTargetId == targetEntity) {

                constTargetPoints = 20f;
            }

            score = (distancePoints + targetTypePriorityPoints + constTargetPoints);
            //Debug.Log($"TargetingSystem: [{attackerEntity}] -> [{score}] = {distancePoints} + {targetTypePriorityPoints} + {constTargetPoints}]");

            return score;
        }

        private int GetTargetTypePriority(TargetType type) {
            return type switch {
                TargetType.Unit => 2,
                TargetType.Building => 1,
                _ => 0
            };
        }

        private void UpdateTargetingComponent(int attackerEntity, ref TargetingComponent targeting,
                                            TargetCandidate? candidate, float currentTime) {

            if (candidate.HasValue == false) {
                var attackerTeam = _teamPool.Value.Get(attackerEntity);
                candidate = (attackerTeam.Value == TeamTypes.Blue) ? _redBase : _blueBase;
                
                //Debug.Log($"<color=green> TargetingSystem: [{attackerEntity}] Set DefaultTarget [{candidate.Value.EntityId}] </color>");
            }

            if (_world.IsEntityAlive(candidate.Value.EntityId) == false)
                return;

            targeting.CurrentTargetId = candidate.Value.EntityId;
            targeting.LastKnownTargetPosition = candidate.Value.Position;
            targeting.LastTargetUpdateTime = currentTime;

            UpdateAttackTargetComponent(attackerEntity, candidate.Value.EntityId);

            //Debug.Log($"<color=orange> TargetingSystem: [{attackerEntity}] Set NewTarget [{candidate.Value.EntityId}] </color>");
        }

        private void UpdateAttackTargetComponent(int attackerEntity, int targetEntity) {
            if (!_modelRadiusPool.Value.Has(targetEntity))
                return;

            var modelRadius = _modelRadiusPool.Value.Get(targetEntity).Value;
            var targetPosition = _positionPool.Value.Get(targetEntity).Value;

            if (_attackTargetPool.Value.Has(attackerEntity)) {
                ref var attackTarget = ref _attackTargetPool.Value.Get(attackerEntity);
                attackTarget.ID = targetEntity;
                attackTarget.LastKnownPosition = targetPosition;
                attackTarget.ModelRadius = modelRadius;
            }
            else {
                _attackTargetPool.Value.Add(attackerEntity) = new AttackTarget {
                    ID = targetEntity,
                    LastKnownPosition = targetPosition,
                    ModelRadius = modelRadius
                };
            }
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