using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.Conditions;
using System;
using UnityEngine;

namespace AtomicFramework.AttackCompanent {

    public sealed class MeleeAttackBehaviour : IEntityInit, IEntityUpdate {
        private SceneEntity _targetEntity;

        private IEntity _entity;
        private int _damage;
        private float _delay;
        private IEvent _attackRequest;
        private IEvent _attackAction;

        private ReactiveVariable<bool> _isWithinReach;
        private ReactiveVariable<bool> _isMoving;
        private ReactiveVariable<bool> _isReloading = new ReactiveVariable<bool>();

        private float _time;
        private IValue<bool> _meleeAttackCondition;
        private ReactiveVariable<bool> _targetIsDead;
        private BaseEvent<float> _targetTakeDamage;
        private ReactiveVariable<Transform> _targetVariable;
        private IEvent _attackTerminate;
        private IEvent<Transform> _targetChangeAction;

        public void Init(IEntity entity) {
            _entity = entity;
            _damage = entity.GetDamage();
            _delay = entity.GetDelay();
            _time = _delay;

            _attackRequest = entity.GetAttackRequest();
            _attackAction = entity.GetAttackAction();
            _attackAction.Subscribe(OnAttack);
            _attackTerminate = entity.GetAttackTerminate();

            _targetChangeAction = entity.GetTargetChanged();
            _targetChangeAction.Subscribe(OnTargetChanged);

            _targetVariable = entity.GetTargetTransform();
            
            if (_targetVariable.Value != null)
                _targetChangeAction?.Invoke(_targetVariable.Value);

            //Debug.Log($"MeleeAttackBehaviour: Inited {entity.Name}");
        }

        public void OnUpdate(IEntity entity, float deltaTime) {

            if (entity.TryGetCanAttack(out IValue<bool> condition) == false) 
                return;

            if (condition == null)
                return;

            if (_isReloading.Value == true) {
                _time -= deltaTime;

                if (_time <= 0) {
                    _time = _delay;
                    _isReloading.Value = false;
                }

                return;
            }

            if (_meleeAttackCondition.Value == true) 
                _attackRequest.Invoke();

            //Debug.Log($"MeleeAttackBehaviour: MeleeAttackCondition {_meleeAttackCondition.Value}");
        }

        private void OnTargetChanged(Transform transform) {

            _targetVariable.Value = transform;

            if (transform.TryGetComponent(out SceneEntity targetEntity) == true) {
                _targetEntity = targetEntity;

                CreateMeleeAttackCondition(_entity);
            }

            //Debug.Log($"MeleeAttackBehaviour: OnTargetChanged {_entity.Name} {_targetVariable.Value}");
        }

        private void OnAttack() {

            if (_meleeAttackCondition.Value == false) {
                _attackTerminate.Invoke();
                return;
            }

            _targetTakeDamage.Invoke(_damage);
            _isReloading.Value = true;
        }

        private void CreateMeleeAttackCondition(IEntity entity) {
            _meleeAttackCondition = CreateAttackContition(entity);
            entity.AddCanAttack(_meleeAttackCondition);
        }

        private IValue<bool> CreateAttackContition(IEntity entity) {
            _isWithinReach = entity.GetIsWithinReach();
            _isReloading = new ReactiveVariable<bool>(false);
            _isMoving = entity.GetIsMoving();

            var attackCondition = new AttackCondition(_isWithinReach, _isReloading);

            if (entity.TryGetTargetTransform(out ReactiveVariable<Transform> target) == true) {

                if (target.Value.TryGetComponent(out SceneEntity targetEntity) == false)
                    throw new ArgumentException($"Entity companent not found!");

                _targetEntity = targetEntity;
                _targetIsDead = _targetEntity.GetIsDeath();
                _targetTakeDamage = _targetEntity.GetTakeDamageAction();

                var attackTargetCondition = new AttackTargetCondition(attackCondition, _targetIsDead);
                var meleeAttackCondition = new MeleeAttackCondition(attackTargetCondition, _isMoving);

                return meleeAttackCondition;
            }

            return attackCondition;
        }
    }
}
