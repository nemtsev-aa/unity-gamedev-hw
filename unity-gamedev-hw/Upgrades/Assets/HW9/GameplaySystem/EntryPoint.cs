using Zenject;
using UnityEngine;
using Conveyors.Entity;

namespace GameplaySystem {

    public sealed class EntryPoint : MonoBehaviour {
        private ConveyorModel _conveyor;

        [Inject]
        private void Construct(ConveyorModel conveyor) {
            _conveyor = conveyor;
        }

        private void Start() {
            StartConveyorWork();
        }

        private void StartConveyorWork() {
            _conveyor.Core.EnableVariable.Current = true;
        }
    }
}