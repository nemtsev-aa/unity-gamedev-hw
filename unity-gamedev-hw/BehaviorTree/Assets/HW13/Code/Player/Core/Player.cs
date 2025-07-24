using UnityEngine;
using BehaviorTree.PlayerVisualSubSystem;

namespace BehaviorTree.PlayerCoreSubsystem {

    public sealed class Player : MonoBehaviour {
        [field: SerializeField] public PlayerConfig Config { get; private set; }
        [field: SerializeField] public PlayerCore Core { get; private set; }
        [field: SerializeField] public PlayerVisual Visual { get; private set; }
        [field: SerializeField] public PlayerUI UI { get; private set; }
        
        public PlayerMediator Mediator { get; private set; }

        public void Init() {
            Core.Init(Config);
            Visual.Init();

            Mediator = new PlayerMediator(Core, Visual);
        }

        private void Update() {
            Mediator.Update(Time.deltaTime);
        }
    }
}