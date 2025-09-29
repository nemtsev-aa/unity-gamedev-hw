using Client.Components.Attack;
using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Targeting;
using Client.Components.Teams;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {
    public sealed class TargetingVisualizationSystem : IEcsInitSystem, IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<AttackerTag, Position, Team, TargetingComponent, VisionComponent>,
            Exc<Inactive>> _attackerFilter;

        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<TargetingComponent> _targetingPool;
        private readonly EcsPoolInject<VisionComponent> _visionPool;
        private readonly EcsPoolInject<Rotation> _rotationPool;

        private bool _debugVisualizationEnabled = true;
        private EcsWorld _world;

        public void Init(IEcsSystems systems) {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems) {
            
            foreach (int attackerEntity in _attackerFilter.Value) {
               
                if (_debugVisualizationEnabled == true) 
                    VisualizeVisionArea(attackerEntity);
            }
        }

        public void SetVisionVisualizationEnabled(bool enabled) {
            _debugVisualizationEnabled = enabled;
        }

        public void UpdateVisionVisualization(int entity) {
            if (_debugVisualizationEnabled == true) {
                VisualizeVisionArea(entity);
            }
        }

        private void VisualizeVisionArea(int attackerEntity) {
            if (!_positionPool.Value.Has(attackerEntity) ||
                !_visionPool.Value.Has(attackerEntity) ||
                !_rotationPool.Value.Has(attackerEntity)) {
                return;
            }

            ref var position = ref _positionPool.Value.Get(attackerEntity);
            ref var vision = ref _visionPool.Value.Get(attackerEntity);
            ref var rotation = ref _rotationPool.Value.Get(attackerEntity);

            Color visionColor = GetTeamColor(attackerEntity);
            visionColor.a = 1f;

            DrawVisionCone(position.Value, rotation.Value, vision.VisionRange, vision.VisionAngle, visionColor);
            DrawVisionRangeCircle(position.Value, vision.VisionRange, visionColor);
            DrawVisionDirection(position.Value, rotation.Value, vision.VisionRange, visionColor);
        }

        private void DrawVisionCone(Vector3 position, Quaternion rotation, float range, float visionAngle, Color color) {
            int segments = Mathf.Max(12, Mathf.CeilToInt(visionAngle / 15f)); // Адаптивное количество сегментов
            float angleStep = visionAngle * Mathf.Deg2Rad / segments;

            Vector3 forward = rotation * Vector3.forward;
            Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * forward;
            Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2f, 0) * forward;

            
            Debug.DrawRay(position, leftBoundary * range, color, 0.1f);
            Debug.DrawRay(position, rightBoundary * range, color, 0.1f);

            Vector3 prevPoint = position + leftBoundary * range;
            for (int i = 1; i <= segments; i++) {
                float angle = -visionAngle / 2f + visionAngle * i / segments;
                Vector3 dir = Quaternion.Euler(0, angle, 0) * forward;
                Vector3 nextPoint = position + dir * range;

                Debug.DrawLine(prevPoint, nextPoint, color, 0.1f);
                Debug.DrawLine(position, nextPoint, color, 0.05f); // Лучи внутри конуса

                prevPoint = nextPoint;
            }

            // Заливка конуса (используем GL для заполнения)
            if (Application.isPlaying) {
                DrawVisionConeFill(position, forward, range, visionAngle, color);
            }
        }

        private void DrawVisionConeFill(Vector3 position, Vector3 forward, float range, float visionAngle, Color color) {
            GL.PushMatrix();
            GL.Begin(GL.TRIANGLES);
            GL.Color(color);

            int segments = 24;
            float halfAngle = visionAngle * 0.5f * Mathf.Deg2Rad;

            Vector3 prevDir = Quaternion.Euler(0, -halfAngle * Mathf.Rad2Deg, 0) * forward;

            for (int i = 1; i <= segments; i++) {
                float angle = -halfAngle + (2 * halfAngle * i / segments);
                Vector3 dir = Quaternion.Euler(0, angle * Mathf.Rad2Deg, 0) * forward;

                GL.Vertex(position);
                GL.Vertex(position + prevDir * range);
                GL.Vertex(position + dir * range);

                prevDir = dir;
            }

            GL.End();
            GL.PopMatrix();
        }

        private void DrawVisionRangeCircle(Vector3 center, float radius, Color color, int segments = 24) {
            float angleStep = 360f / segments;
            Vector3 prevPoint = center + Quaternion.Euler(0, 0, 0) * Vector3.forward * radius;

            for (int i = 1; i <= segments; i++) {
                float angle = i * angleStep;
                Vector3 nextPoint = center + Quaternion.Euler(0, angle, 0) * Vector3.forward * radius;
                Debug.DrawLine(prevPoint, nextPoint, color, 0.1f);
                prevPoint = nextPoint;
            }
        }

        private void DrawVisionDirection(Vector3 position, Quaternion rotation, float range, Color color) {
            Vector3 forward = rotation * Vector3.forward;
            Debug.DrawRay(position, forward * range * 0.5f, Color.yellow, 0.1f); // Желтая линия направления

            // Стрелка направления
            Vector3 arrowEnd = position + forward * range * 0.5f;
            Vector3 arrowRight = Quaternion.Euler(0, 135, 0) * forward * 0.5f;
            Vector3 arrowLeft = Quaternion.Euler(0, -135, 0) * forward * 0.5f;

            Debug.DrawLine(arrowEnd, arrowEnd + arrowRight, Color.yellow, 0.1f);
            Debug.DrawLine(arrowEnd, arrowEnd + arrowLeft, Color.yellow, 0.1f);
        }

        private Color GetTeamColor(int entity) {
            if (!_teamPool.Value.Has(entity))
                return Color.white;

            return _teamPool.Value.Get(entity).Value switch {
                TeamTypes.Blue => Color.blue,
                TeamTypes.Red => Color.red,
                _ => Color.white
            };
        }

        private void DrawTargetLine(int attackerEntity) {
            if (!_targetingPool.Value.Has(attackerEntity) ||
                !_positionPool.Value.Has(attackerEntity))
                return;

            ref var targeting = ref _targetingPool.Value.Get(attackerEntity);
            ref var position = ref _positionPool.Value.Get(attackerEntity);

            if (targeting.CurrentTargetId != -1 &&
                _world.IsEntityAlive(targeting.CurrentTargetId) &&
                _positionPool.Value.Has(targeting.CurrentTargetId)) {
                Vector3 targetPos = _positionPool.Value.Get(targeting.CurrentTargetId).Value;
                Debug.DrawLine(position.Value, targetPos, Color.red, 0.1f);

                // Маркер цели
                Debug.DrawLine(targetPos + Vector3.up * 2f, targetPos - Vector3.up * 2f, Color.red, 0.1f);
                Debug.DrawLine(targetPos + Vector3.right * 2f, targetPos - Vector3.right * 2f, Color.red, 0.1f);
                Debug.DrawLine(targetPos + Vector3.forward * 2f, targetPos - Vector3.forward * 2f, Color.red, 0.1f);
            }
        }
    }
}