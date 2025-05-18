using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;

namespace ZombieShooter.SceneObjects {

    public sealed class WeaponController : IContextInstaller, IContextDispose {
        private IEvent _characterAttackAction;
        private IEvent _weaponAttackAction;

        public void Install(IContext context) {

            var characterEntity = context.GetCharacter();
            _characterAttackAction = characterEntity.GetAttackAction();
            _characterAttackAction.Subscribe(OnCharacterAttackAction);

            var weaponEntity = context.GetWeapon();
            _weaponAttackAction = weaponEntity.GetAttackAction();
        }

        private void OnCharacterAttackAction() {
            //Debug.Log($"WeaponController: OnCharacterAttackAction");
            _weaponAttackAction.Invoke();
        }

        public void Dispose(IContext context) {
            _characterAttackAction.Unsubscribe(OnCharacterAttackAction);
        }
    }
}
