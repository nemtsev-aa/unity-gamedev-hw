using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.BulletSystem;
using AtomicFramework.Conditions;
using System;
using UnityEngine;
using ZombieShooter.GameCycleSystem;

namespace ZombieShooter.SceneObjects {

    [Serializable]
    public sealed class CharacterSpawner : IContextInit,
                                           IGameStartListener,
                                           IGameFinishListener {

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private WeaponConfig _weaponConfig;

        private IContext _context;
        private CharacterFactory _characterFactory;
        private WeaponFactory _weaponFactory;

        private Character _currentCharacter;
        private Weapon _currentWeapon;

        public bool IsActive { get; private set; }
        public bool IsPause { get; private set; }

        public void Init(IContext context) {
            _context = context;
            _characterFactory = new CharacterFactory(_characterConfig.Prefab);
            _weaponFactory = new WeaponFactory(_weaponConfig.Prefab);

            _context.AddCharactreCreaetEvent(new BaseEvent());
        }

        public void OnStartGame() {
            CreateCharacter();
            CreateWeapon();

            CharacterAttackCondition();
            WeaponAttackCondition();

            _context.GetCharactreCreaetEvent().Invoke();
        }

        public void OnFinishGame() {
            _currentCharacter.Entity.DelCanAttack();

            UnityEngine.Object.Destroy(_currentCharacter.gameObject);
            UnityEngine.Object.Destroy(_currentWeapon.gameObject);

            _context.DelCharacter();
            _context.DelWeapon();
        }

        private void CreateCharacter() {
            _currentCharacter = _characterFactory.Get(_spawnPoint);
            _context.SetCharacter(_currentCharacter.Entity);

            _currentCharacter.Init(_characterConfig);
            _currentCharacter.Entity.Install();
        }

        private void CreateWeapon() {
            _currentWeapon = _weaponFactory.Get(_spawnPoint);
            _context.SetWeapon(_currentWeapon.Entity);

            _currentWeapon.SetFirePoint(_currentCharacter.FirePoint);
            _currentWeapon.Init(_weaponConfig);
        }

        private void CharacterAttackCondition() {
            var entity = _currentCharacter.Entity;
            var isDeath = entity.GetIsDeath();

            entity.AddCanAttack(new CharacterAttackCondition(isDeath));
        }

        private void WeaponAttackCondition() {
            var wac = _currentWeapon.Entity.GetCanRangeAttack();
            var uac = _currentCharacter.Entity.GetCanAttack();

            var canAttack = new WeaponAttackCondition(wac, uac);
            _currentWeapon.Entity.AddCanAttack(canAttack);
        }
    }
}
