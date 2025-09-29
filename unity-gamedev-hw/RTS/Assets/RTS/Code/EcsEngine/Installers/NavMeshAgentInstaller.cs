using UnityEngine;
using Leopotam.EcsLite.Entities;
using Client.Components.Movement;
using UnityEngine.AI;
using Client.Installer;
using Random = UnityEngine.Random;

namespace Code.Visual {

    public sealed class NavMeshAgentInstaller : EntityInstaller {
        [SerializeField] private NavMeshAgent _agent;

        private UnitBaseConfig _config;
        private Entity _entity;

        public void Init(UnitBaseConfig config) {
             _config = config;
        }

        protected override void Install(Entity entity) {
            _entity = entity;

            // Настраиваем параметры агента
            _agent.speed = _config.MoveSpeed;
            _agent.angularSpeed = _config.RotationSpeed;
            _agent.acceleration = _config.Acceleration;
            _agent.stoppingDistance = _config.StoppingDistance;
            _agent.autoBraking = _config.AutoBraking;
            _agent.avoidancePriority = Random.Range(1, 100); // Разный приоритет для лучшего избегания

            // Параметры избегания столкновений
            _agent.radius = _config.ModelRadius;
            _agent.height = 1f;
            _agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            _agent.autoRepath = true;
            _agent.autoTraverseOffMeshLink = false;

            entity.AddData(new NavMeshAgentComponent {
                Agent = _agent,
                StoppingDistance = _config.StoppingDistance,
            });

            // Устанавливаем начальную позицию
            _agent.Warp(entity.transform.position);
        }

        protected override void Dispose(Entity entity) {

        }
    }
}


