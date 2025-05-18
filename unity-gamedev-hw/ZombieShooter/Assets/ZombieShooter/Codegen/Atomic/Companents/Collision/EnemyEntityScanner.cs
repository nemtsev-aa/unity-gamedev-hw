using Atomic.Elements;
using Atomic.Entities;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.CollisionMechanics {

    public sealed class EnemyEntityScanner : UnitScanner, IEntityInit, IEntityUpdate {
        private CollisionBehavior<Enemy> _behavior;

        private IEntity _entity;
        private ReactiveVariable<bool> _canScane = new ReactiveVariable<bool>();

        public EnemyEntityScanner(IDetectionBehavior detectionBehavior) : base(detectionBehavior) {
            _behavior = (CollisionBehavior<Enemy>)detectionBehavior;
        }

        public void Init(IEntity entity) {
            _entity = entity;
            _behavior.Init(entity);

            _canScane = entity.GetCanScane();
            _canScane.Subscribe(OnCanScane);
        }

        public void OnUpdate(IEntity entity, float deltaTime) {
            _behavior.OnUpdate(entity, deltaTime);
        }

        private void OnCanScane(bool status) {

            if (status == true) {
                var closestUnit = (Enemy)FindClosest();

                if (closestUnit != null) {
                    _entity.SetTargetTransform(closestUnit.transform);
                    _entity.GetTargetChanged().Invoke(closestUnit.transform);
                    //Debug.Log($"SetTargetTransform: {_entity.Name} {_entity.GetTargetTransform().Value.gameObject.name}");
                }
            }
        }
    }
}