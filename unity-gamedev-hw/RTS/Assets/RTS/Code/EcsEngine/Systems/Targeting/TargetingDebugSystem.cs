using Client.Components.Attack;
using Client.Components.Movement;
using Client.Components.Targeting;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems.Spatial {

    public sealed class TargetingDebugSystem : IEcsRunSystem {
        private readonly EcsFilterInject<Inc<TargetingComponent, Position>> _targetingFilter;
        private readonly EcsFilterInject<Inc<AttackTarget, Position>> _attackTargetFilter;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<TargetingComponent> _targetingPool;
        private readonly EcsPoolInject<AttackTarget> _attackTargetPool;

        public void Run(IEcsSystems systems) {
            
            if (Debug.isDebugBuild == false)
                return;

            DrawTargetingLines();
            DrawAttackTargets();
        }

        private void DrawTargetingLines() {
            
            foreach (int entity in _targetingFilter.Value) {
                var position = _positionPool.Value.Get(entity).Value;
                var targeting = _targetingPool.Value.Get(entity);

                if (targeting.CurrentTargetId != -1) {
                    Debug.DrawLine(position, targeting.LastKnownTargetPosition,
                                 targeting.CurrentTargetId != -1 ? Color.red : Color.gray,
                                 0.1f);
                }
            }
        }

        private void DrawAttackTargets() {

            foreach (int entity in _attackTargetFilter.Value) {
                var position = _positionPool.Value.Get(entity).Value;
                var attackTarget = _attackTargetPool.Value.Get(entity);

                Debug.DrawLine(position, attackTarget.LastKnownPosition, Color.yellow, 0.1f);

                DrawCircle(attackTarget.LastKnownPosition, attackTarget.ModelRadius, Color.yellow);
            }
        }

        private void DrawCircle(Vector3 center, float radius, Color color, int segments = 12) {
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

