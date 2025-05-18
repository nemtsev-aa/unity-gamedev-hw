using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;

namespace AtomicFramework.View.Visual {

    public sealed class AnimatorBehaviour : IEntityInit, IEntityDispose {
        private static readonly int s_isDeath = Animator.StringToHash("IsDeath");
        private static readonly int s_moveDirection = Animator.StringToHash("MoveDirection");
        private static readonly int s_attackRequest = Animator.StringToHash("Attack");
        private static readonly int s_stunAction = Animator.StringToHash("IsStun");

        public const string ATTACK_EVENT_NAME = "attack";
        public const string DEATH_EVENT_NAME = "death";

        private Animator _animator;
        private AnimationDispatcher _animationDispatcher;
        private ReactiveVariable<bool> _isDeath;
        private BaseEvent<IEntity> _isDestroy;
        private IEvent _attackRequest;
        private IEvent _attackAction;
        private IEvent _attackTerminate;
        private IEvent<bool> _stunAction;

        private ReactiveVariable<Vector3> _moveDirection;

        public void Init(IEntity entity) {
            _animator = entity.GetAnimator();
            _isDeath = entity.GetIsDeath();
            _isDestroy = entity.GetIsDestroy();

            _animationDispatcher = entity.GetAnimationDispatcher();

            _attackRequest = entity.GetAttackRequest();
            _attackRequest.Subscribe(OnAttackRequested);
            _attackAction = entity.GetAttackAction();

            if (entity.TryGetAttackTerminate(out IEvent attackTerminate) == true) {
                _attackTerminate = attackTerminate;
                _attackTerminate.Subscribe(OnAttackTerminated);
            }

            _moveDirection = entity.GetMoveDirection();
            _moveDirection.Subscribe(OnMoveDirectionChanged);

            _animator.SetBool(s_isDeath, _isDeath.Value);
            _isDeath.Subscribe(OnIsDeathChanged);

            if (entity.TryGetStunAction(out IEvent<bool> stunAction) == true) {
                _stunAction = stunAction;
                _stunAction.Subscribe(OnStunAction);
            }

            _animationDispatcher.OnEventReceived += OnEventReceived;
        }

        private void OnMoveDirectionChanged(Vector3 direction) {

            if (direction == Vector3.forward)
                _animator.SetFloat(s_moveDirection, 1f);
            else if (direction == Vector3.back)
                _animator.SetFloat(s_moveDirection, 2f);
            else if (direction == Vector3.right)
                _animator.SetFloat(s_moveDirection, 3f);
            else if (direction == Vector3.left)
                _animator.SetFloat(s_moveDirection, 4f);
            else if (direction == Vector3.zero)
                _animator.SetFloat(s_moveDirection, 0f);
            else
                _animator.SetFloat(s_moveDirection, 1f);
        }

        private void OnIsDeathChanged(bool isDead) {
            _animator.SetBool(s_isDeath, isDead);
        }

        private void OnAttackRequested() {
            _animator.SetTrigger(s_attackRequest);
        }

        private void OnAttackTerminated() {
            _animator.ResetTrigger(s_attackRequest);
        }

        private void OnEventReceived(string eventName) {

            if (eventName == ATTACK_EVENT_NAME) {
                _attackAction.Invoke();
                OnAttackTerminated();
            }

            if (eventName == DEATH_EVENT_NAME) {

                if (_animator.gameObject.TryGetComponent(out SceneEntityProxy proxy) == true)
                    _isDestroy.Invoke(proxy.source);
            }
        }

        private void OnStunAction(bool status) {
            _animator.SetBool(s_stunAction, status);
        }

        public void Dispose(IEntity entity) {
            _isDeath?.Unsubscribe(OnIsDeathChanged);
            _attackRequest?.Unsubscribe(OnAttackRequested);
            _moveDirection?.Unsubscribe(OnMoveDirectionChanged);

            _animationDispatcher.OnEventReceived -= OnEventReceived;
            _stunAction?.Unsubscribe(OnStunAction);
        }
    }
}
