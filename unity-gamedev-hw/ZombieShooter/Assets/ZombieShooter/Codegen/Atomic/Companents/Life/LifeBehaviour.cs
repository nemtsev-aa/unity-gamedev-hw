using Atomic.Elements;
using Atomic.Entities;

namespace AtomicFramework.ZombieShooter {

    public sealed class LifeBehaviour : IEntityInit, IEntityDispose {
        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<float> _hitPoints;
        private BaseEvent<float> _takeDamageAction;

        public void Init(IEntity entity) {
            _isDead = entity.GetIsDeath();
            _hitPoints = entity.GetHitPoints();
            _takeDamageAction = entity.GetTakeDamageAction();

            _takeDamageAction.Subscribe(OnTakeDamage);
        }

        private void OnTakeDamage(float damage) {

            if (_isDead.Value == true)
                return;

            _hitPoints.Value -= damage;
            
            if (_hitPoints.Value <= 0) {
                _hitPoints.Value = 0;
                _isDead.Value = true;
            }
        }

        public void Dispose(IEntity entity) {
            _takeDamageAction.Unsubscribe(OnTakeDamage);
        }
    }
}
