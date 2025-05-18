using Atomic.Elements;
using Atomic.Entities;

namespace AtomicFramework.ShootCompanent {

    public sealed class ShootBehaviour : IEntityUpdate, IEntityInit, IEntityDispose {
        private readonly float _reloadTime;
        private readonly ShootComponent _shootComponent;

        private IEvent _attackAction;
        private float _reloadTimer;

        public ReactiveVariable<bool> IsReloading = new ReactiveVariable<bool>();

        public ShootBehaviour(ShootComponent shootComponent, float reloadTime) {
            _shootComponent = shootComponent;
            _reloadTime = reloadTime;
        }

        public void Init(IEntity entity) {

            _attackAction = entity.GetAttackAction();
            _attackAction.Subscribe(OnAttackAction);

            _reloadTimer = _reloadTime;
        }

        public void OnUpdate(IEntity entity, float deltaTime) {

            if (IsReloading.Value == true) {
                _reloadTimer -= deltaTime;

                if (_reloadTimer <= 0) {
                    _reloadTimer = _reloadTime;

                    IsReloading.Value = false;
                }
            }
        }

        private void OnAttackAction() {
            _shootComponent.Shoot();

            _reloadTimer = _reloadTime;
            IsReloading.Value = true;
        }

        public void Dispose(IEntity entity) {
            _attackAction.Unsubscribe(OnAttackAction);
        }
    }
}
