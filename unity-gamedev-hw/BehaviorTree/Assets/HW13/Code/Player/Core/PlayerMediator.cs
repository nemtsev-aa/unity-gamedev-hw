using BehaviorTree.PlayerVisualSubSystem;

namespace BehaviorTree.PlayerCoreSubsystem {
    
    public sealed class PlayerMediator {
        private const string STATE_INDEX = "StateIndex";

        private readonly PlayerCore _core;
        private readonly PlayerVisual _visual;

        public PlayerMediator(PlayerCore core, PlayerVisual visual) {
            _core = core;
            _visual = visual;

            _visual.InventoryVisual.Init(_core.Inventory);
            ShowInventoryVisual(true);
        }

        public void Update(float deltaTime) {

            var currentVelocity = GetCurrentVelocity();
            var cargoAmount = GetCargoAmount();
            var animatorState = GetAnimatorState();

            ShowVillagerAxe(animatorState == 1f);

            if (currentVelocity == 0) {
                _visual.SetPlayerAnimatorStates(PlayerAnimatorStates.Idle);
                return;
            }

            if (currentVelocity > 0 && cargoAmount == 0) {
                _visual.SetPlayerAnimatorStates(PlayerAnimatorStates.MoveToForest);
                return;
            }

            if (currentVelocity > 0 && cargoAmount > 0) {
                _visual.SetPlayerAnimatorStates(PlayerAnimatorStates.Delivery);
                return;
            }
        }

        private float GetCurrentVelocity() {
            return _core.Mover.Velocity;
        }

        private int GetCargoAmount() {
            return _core.Inventory.CurrentAmount;
        }

        private float GetAnimatorState() {
            return _visual.Animator.GetFloat(STATE_INDEX); 
        }

        private void ShowInventoryVisual(bool status) {
            _visual.InventoryVisual.gameObject.SetActive(status);
        }

        private void ShowVillagerAxe(bool status) {
            _visual.VillagerAxe.gameObject.SetActive(status);
        }
    }
}