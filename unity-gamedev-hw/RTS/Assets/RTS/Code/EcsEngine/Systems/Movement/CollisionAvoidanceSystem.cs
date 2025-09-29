using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Teams;
using Client.Components.Visual;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {

    public sealed class CollisionAvoidanceSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<Position, MoveDirection, ModelRadius, Team>,
            Exc<Inactive>> _unitsFilter;

        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<MoveDirection> _directionPool;
        private readonly EcsPoolInject<ModelRadius> _radiusPool;
        private readonly EcsPoolInject<Team> _teamPool;

        public void Run(IEcsSystems systems) {
            foreach (int entity in _unitsFilter.Value) {
                ref var position = ref _positionPool.Value.Get(entity);
                ref var moveDirection = ref _directionPool.Value.Get(entity);
                ref var radius = ref _radiusPool.Value.Get(entity);
                var team = _teamPool.Value.Get(entity);

                Vector3 avoidanceForce = CalculateAvoidanceForce(entity, position.Value, radius.Value, team.Value);

                if (avoidanceForce != Vector3.zero) {
                    // Смешиваем основное направление с силой избегания
                    Vector3 newDirection = (moveDirection.Value + avoidanceForce * 2f).normalized;
                    moveDirection.Value = newDirection;
                }
            }
        }

        private Vector3 CalculateAvoidanceForce(int currentEntity, Vector3 position, float radius, TeamTypes team) {
            Vector3 avoidanceForce = Vector3.zero;
            int neighborCount = 0;
            float avoidanceRadius = radius * 3f; // Радиус обнаружения препятствий

            foreach (int otherEntity in _unitsFilter.Value) {
                if (otherEntity == currentEntity) continue;

                ref var otherPosition = ref _positionPool.Value.Get(otherEntity);
                ref var otherRadius = ref _radiusPool.Value.Get(otherEntity);
                var otherTeam = _teamPool.Value.Get(otherEntity);

                // Игнорируем юнитов другой команды для избегания
                if (otherTeam.Value != team) continue;

                float distance = Vector3.Distance(position, otherPosition.Value);
                float combinedRadius = radius + otherRadius.Value;

                if (distance < avoidanceRadius && distance > 0) {
                    // Расчет силы отталкивания
                    Vector3 awayDirection = (position - otherPosition.Value).normalized;
                    float strength = 1f - Mathf.Clamp01(distance / avoidanceRadius);

                    avoidanceForce += awayDirection * strength;
                    neighborCount++;
                }
            }

            if (neighborCount > 0) {
                avoidanceForce /= neighborCount;
            }

            return avoidanceForce;
        }
    }
}

