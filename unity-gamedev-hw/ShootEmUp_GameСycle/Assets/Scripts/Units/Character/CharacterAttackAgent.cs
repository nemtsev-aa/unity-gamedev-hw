using UnityEngine;

namespace ShootEmUp {
    public sealed class CharacterAttackAgent {
        private Character _character;
        private BulletSystem _bulletSystem;
        private BulletConfig _bulletConfig;

        private bool _fireRequired;
        private BulletSystem.Args _curreentBulletArgs;

        public CharacterAttackAgent(Character character, BulletSystem bulletSystem, BulletConfig bulletConfig) {
            _character = character;
            _bulletSystem = bulletSystem;
            _bulletConfig = bulletConfig;

            CreateCurreentBulletArgs();
        }

        public void SetFireRequired(bool status) {
            _fireRequired = status;
            Fire();
        }

        private void CreateCurreentBulletArgs() {
            var weapon = _character.Weapon;

            _curreentBulletArgs = new BulletSystem.Args {
                IsPlayer = true,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = weapon.Position,
                Velocity = weapon.Rotation * Vector3.up * _bulletConfig.Speed
            };
        }

        private void Fire() {
            if (_fireRequired == true) {
                OnFlyBullet();
                _fireRequired = false;
            }
        }

        private void OnFlyBullet() {
            _curreentBulletArgs.Position = _character.Weapon.Position;
            _bulletSystem.FlyBulletByArgs(_curreentBulletArgs);
        }
    }
}
