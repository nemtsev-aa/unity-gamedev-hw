using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.BulletSystem;
using AtomicFramework.ShootCompanent;
using System;
using UnityEngine;

namespace AtomicFramework.MoveCompanent {

    [Serializable]
    public sealed class AssaultRifleBehaviour : IEntityInit, IEntityUpdate, IEntityDispose {
        private readonly float _addBulletDelay;
        private readonly float _reloadTime;

        private Transform _target;
        private IEntity _entity;
        private ReactiveVariable<int> _currentBulletAmount;
        private ReactiveVariable<int> _maxBulletAmount;
        private ReactiveVariable<bool> _isReloaded;
        private ReactiveVariable<bool> _isRefill;

        private float _addBulletTimer;
        private float _reloadTimer;

        private IEvent _attackAction;
        private IEvent<Bullet> _attackEvent;

        public AssaultRifleBehaviour(float addBulletDelay, float reloadTime) {
            _isRefill = new ReactiveVariable<bool>();
            _isReloaded = new ReactiveVariable<bool>();

            _addBulletDelay = addBulletDelay;
            _addBulletTimer = _addBulletDelay;
            _reloadTime = reloadTime;
        }

        public void Init(IEntity entity) {
            if (_attackAction != null)
                return;

            _entity = entity;
            _currentBulletAmount = entity.GetCurrentBulletAmount();
            _maxBulletAmount = entity.GetMaxBulletAmount();

            _attackAction = entity.GetAttackAction();
            _attackAction.Subscribe(OnAttackAction);

            _attackEvent = entity.GetAttackEvent();
            _attackEvent.Subscribe(OnAttackEvent);

            _reloadTimer = _reloadTime;

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

            if (_isReloaded.Value == false) {
                _reloadTimer -= deltaTime;

                if (_reloadTimer <= 0) {
                    _reloadTimer = _reloadTime;

                    _isReloaded.Value = true;
                }
            }
        }

        private void OnAttackAction() {
            //Debug.Log($"AssaultRifleBehaviour OnAttackAction");

            if (_currentBulletAmount.Value > 0)
                _currentBulletAmount.Value -= 1;

            _reloadTimer = _reloadTime;
            _isRefill.Value = true;

            _isReloaded.Value = false;
        }

        private void OnAttackEvent(Bullet bullet) {
            var firePoint = _entity.GetFirePoint().transform; 

            bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            var entity = bullet.Entity;
            entity.SetMoveDirection(firePoint.forward);
            entity.SetIsMoving(true);
            entity.GetIsDeath().Value = false;

            //Debug.Log($"AssaultRifleBehaviour: OnAttackEvent {bullet.Entity.Name} {firePoint.forward}");
        }

        private void CreateRangeAttackCondition(IEntity entity) {
            var shootCondition = new ShootCondition(_isReloaded, _currentBulletAmount);
            entity.AddCanRangeAttack(shootCondition);
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
