using System;
using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.ShootCompanent;
using AtomicFramework.Conditions;

namespace AtomicFramework.AttackCompanent {

    [Serializable]
    public sealed class RangeWeaponBehaviour : IEntityInit, IEntityUpdate, IEntityDispose {
        private readonly float _addBulletDelay;
        private readonly ShootBehaviour _shootBehaviour;

        private IEvent _attackAction;

        private Transform _target;
        private ReactiveVariable<int> _currentBulletAmount;
        private ReactiveVariable<int> _maxBulletAmount;
        private ReactiveVariable<bool> _isRefill;

        private float _addBulletTimer;

        public RangeWeaponBehaviour(float addBulletDelay, ShootBehaviour shootBehaviour) {
            _isRefill = new ReactiveVariable<bool>();

            _shootBehaviour = shootBehaviour;
            _addBulletDelay = addBulletDelay;
            _addBulletTimer = _addBulletDelay;
        }

        public void Init(IEntity entity) {
            if (_attackAction != null)
                return;

            _shootBehaviour.Init(entity);

            _currentBulletAmount = entity.GetCurrentBulletAmount();
            _maxBulletAmount = entity.GetMaxBulletAmount();
  
            _attackAction = entity.GetAttackAction();
            _attackAction.Subscribe(OnAttackAction);

            CreateRangeAttackCondition(entity);
        }

        public void OnUpdate(IEntity entity, float deltaTime) {
            
            if (_isRefill.Value == true) {
                _addBulletTimer -= deltaTime;

                if (_addBulletTimer <= 0) {
                    _addBulletTimer = _addBulletDelay;               
                    AddBullet();
                }
            }

            _shootBehaviour.OnUpdate(entity, deltaTime);
        }

        private void OnAttackAction() {
            if (_currentBulletAmount.Value > 0)
                _currentBulletAmount.Value -= 1;

            _isRefill.Value = true;
        }

        private void CreateRangeAttackCondition(IEntity entity) {
            var attackCondition = CreateAttackContition(entity);
            var shootCondition = new ShootCondition(attackCondition, _currentBulletAmount);

            entity.AddCanRangeAttack(shootCondition);
        }

        private IValue<bool> CreateAttackContition(IEntity entity) {
            var _isWithinReach = entity.GetIsWithinReach();
            var _isReloading = _shootBehaviour.IsReloading;

            var attackCondition = new AttackCondition(_isWithinReach, _isReloading);

            if (entity.TryGetTargetTransform(out ReactiveVariable<Transform> target) == true) {
                
                if (target.Value.TryGetComponent(out SceneEntity targetEntity) == false)
                    throw new ArgumentException($"Entity companent not found!");

                _target = target.Value;
                var targetIsDead = targetEntity.GetIsDeath();
                var attackTargetCondition = new AttackTargetCondition(attackCondition, targetIsDead);

                return attackTargetCondition;
            }

            return attackCondition;
        }

        private void AddBullet() {

            if (_currentBulletAmount.Value == _maxBulletAmount.Value) {
                _isRefill.Value = false;
                return;
            }

            _currentBulletAmount.Value += 1;
        }

        public void Dispose(IEntity entity) {
            _attackAction.Unsubscribe(OnAttackAction);
        }
    }
}
