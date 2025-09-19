using Zenject;
using UnityEngine;
using Conveyors.Entity.Core;
using Conveyors.Entity.Visual;
using Game.GameEngine.GameResources;

namespace Conveyors.Entity {

    public sealed partial class ConveyorModel : MonoBehaviour {
        [field: SerializeField] public ConveyourConfig Config { get; private set; }
        [field: SerializeField] public ConveyorCore Core { get; private set; }
        [field: SerializeField] public ConveyorVisual Visual { get; private set; }
        [field: SerializeField] public ConveyorCanvas Canvas { get; private set; }

        private IFixedUpdateListener _workMechanics;

        [Inject]
        public void Construct(ResourceInfoCatalog resourceCatalog) {
            Core.Init(Config);
            Visual.Init(Core);
            Canvas.Init(Config, resourceCatalog, Core);

            _workMechanics = Core.WorkMechanics;
        }

        private void FixedUpdate() {

            if (Core.EnableVariable.Current == false)
                return;
                
            _workMechanics.FixedUpdate(Time.deltaTime);
        }
    }
}