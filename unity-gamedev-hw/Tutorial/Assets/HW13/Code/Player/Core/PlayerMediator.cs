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
            ShowCurrentAnimation();
        }

        private void ShowCurrentAnimation() {
            var haveTarget = CheckCurrentTarget();
            var currentVelocity = GetCurrentVelocity();
            var cargoAmount = GetCargoAmount();

            ShowCurrentTools(haveTarget);

            if (haveTarget == true) {

                if (_visual.Animator.GetFloat(STATE_INDEX) != 1f)
                    _visual.SetPlayerAnimatorStates(PlayerAnimatorStates.Felling);
                
                return;
            }

            if (currentVelocity == 0) {
                _visual.SetPlayerAnimatorStates(PlayerAnimatorStates.Idle);
                return;
            }

            if (currentVelocity > 0) {

                if (cargoAmount == 0) {
                    _visual.SetPlayerAnimatorStates(PlayerAnimatorStates.MoveToForest);
                    return;
                }

                _visual.SetPlayerAnimatorStates(PlayerAnimatorStates.Delivery);
            }
        }

        private bool CheckCurrentTarget() {
            return (_core.Feller.Target != null);
        }

        private float GetCurrentVelocity() {
            return _core.Mover.Velocity;
        }

        private int GetCargoAmount() {
            return _core.Inventory.CurrentAmount;
        }

        private void ShowInventoryVisual(bool status) {
            _visual.InventoryVisual.gameObject.SetActive(status);
        }

        private void ShowCurrentTools(bool status) {
            _visual.VillagerAxe.gameObject.SetActive(status);
        }

    }
}