using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Targeting;
using Client.Components.Teams;
using Client.Components.Visual;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {
    public sealed class AnticipationSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<Position, TargetingComponent, MoveDirection, ModelRadius, Team>,
            Exc<Inactive>> _unitsFilter;

        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<TargetingComponent> _targetingPool;
        private readonly EcsPoolInject<MoveDirection> _directionPool;
        private readonly EcsPoolInject<ModelRadius> _radiusPool;
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<MoveSpeed> _speedPool;

        public void Run(IEcsSystems systems) {
            foreach (int entity in _unitsFilter.Value) {
                ref var position = ref _positionPool.Value.Get(entity);
                ref var targeting = ref _targetingPool.Value.Get(entity);
                ref var moveDirection = ref _directionPool.Value.Get(entity);
                ref var radius = ref _radiusPool.Value.Get(entity);
                var team = _teamPool.Value.Get(entity);

                if (targeting.CurrentTargetId == -1)
                    continue;

                // Получаем позицию цели
                var targetPosition = GetTargetPosition(targeting.CurrentTargetId);
                if (targetPosition == null)
                    continue;

                // Предсказываем будущую позицию цели
                Vector3 anticipatedPosition = PredictTargetMovement(
                    targeting.CurrentTargetId,
                    targetPosition.Value,
                    1.0f // Предсказание на 1 секунду вперед
                );

                // Корректируем направление движения с учетом предсказания
                Vector3 toAnticipatedTarget = (anticipatedPosition - position.Value).normalized;
                moveDirection.Value = Vector3.Lerp(moveDirection.Value, toAnticipatedTarget, 0.3f);
            }
        }

        private Vector3? GetTargetPosition(int targetEntity) {
            if (_positionPool.Value.Has(targetEntity))
                return _positionPool.Value.Get(targetEntity).Value;

            return null;
        }

        private Vector3 PredictTargetMovement(int targetEntity, Vector3 currentPosition, float timeAhead) {
            // Если цель движется, предсказываем ее позицию
            if (_directionPool.Value.Has(targetEntity) && _speedPool.Value.Has(targetEntity)) {
                Vector3 targetDirection = _directionPool.Value.Get(targetEntity).Value;
                float targetSpeed = _speedPool.Value.Get(targetEntity).Value;
                return currentPosition + targetDirection * targetSpeed * timeAhead;
            }

            return currentPosition; // Если не движется, возвращаем текущую позицию
        }
    }
}

