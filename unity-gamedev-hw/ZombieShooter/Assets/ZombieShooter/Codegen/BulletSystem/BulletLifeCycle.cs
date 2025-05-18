using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.BulletSystem {

    public sealed class BulletLifeCycle : IEntityInit, IEntityUpdate, IEntityDispose {
        private IEntity _entity;

        private Transform _root;
        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;
        private int _damage;
        private float _duration;
        private IEvent<Transform> _targetChangeAction;
        private Transform _currentTarget;

        private BaseEvent<IEntity> _isDestroy;
        private ReactiveVariable<bool> _isMoving;
        private ReactiveVariable<bool> _isDeath;
        private Cycle _lifeCicle;

        public void Init(IEntity entity) {
            _entity = entity;

            _root = _entity.GetRoot();
            _root.transform.GetPositionAndRotation(out Vector3 position, out Quaternion rotation);
            _defaultPosition = position;
            _defaultRotation = rotation;

            _damage = _entity.GetDamage();
            _duration = _entity.GetRange();
            _isMoving = _entity.GetIsMoving();
            _isDeath = _entity.GetIsDeath();
            _isDestroy = _entity.GetIsDestroy();
            _targetChangeAction = _entity.GetTargetChanged();

            _isDestroy.Subscribe(OnDestroyed);
            _targetChangeAction.Subscribe(OnTargetChanged);

            _lifeCicle = new Cycle(_duration);

            StartLifeCycle(_entity);
        }

        public void OnUpdate(IEntity entity, float deltaTime) {
            if (_lifeCicle.CurrentState == Cycle.State.IDLE && _isMoving.Value == true)
                _lifeCicle.Start();

            if (_lifeCicle.CurrentState == Cycle.State.PLAYING)
                _lifeCicle.Tick(deltaTime);
        }

        private void StartLifeCycle(IEntity bullet) {
            _lifeCicle.SetCurrentTime(0);

            _lifeCicle.Start();
            _lifeCicle.OnCycle += OnLifeCycleEnded;
        }

        private void OnLifeCycleEnded() {
            _lifeCicle.Stop();
            _isDestroy.Invoke(_entity);
        }

        private void OnTargetChanged(Transform transform) {
            if (_currentTarget == transform)
                return;

            _currentTarget = transform;

            if (transform.TryGetComponent(out Enemy enemy) == true) {
                var targetEntity = enemy.Entity;
                var targetIsDeath = targetEntity.GetIsDeath().Value;


                if (targetIsDeath == false) {
                    var targetTakeDamage = targetEntity.GetTakeDamageAction();
                    targetTakeDamage.Invoke(_damage);

                    _isDestroy.Invoke(_entity);
                    _isDeath.Value = true;
                }
            }
        }

        private void OnDestroyed(IEntity bulletEntity) {
            _currentTarget = null;

            _root.transform.position = _defaultPosition;
            _root.transform.rotation = _defaultRotation;
        }

        public void Dispose(IEntity entity) {
            _targetChangeAction.Unsubscribe(OnTargetChanged);
            _lifeCicle.OnCycle -= OnLifeCycleEnded;
        }
    }
}
