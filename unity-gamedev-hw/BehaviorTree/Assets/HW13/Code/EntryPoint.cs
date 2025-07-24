using Zenject;
using UnityEngine;
using FarmingSystem;
using Conveyors.Entity;

namespace GameplaySystem {

    public sealed class EntryPoint : MonoBehaviour {
        private ConveyorModel _conveyor;
        private FellingZone _fellingZone;

        [Inject]
        private void Construct(ConveyorModel conveyor,
                               FellingZone fellingZone) {

            _conveyor = conveyor;
            _fellingZone = fellingZone;
        }

        private void Start() {
            StartConveyorWork();
            InitFellingZone();
        }

        private void StartConveyorWork() {
            _conveyor.Core.EnableVariable.Current = true;
        }

        private void InitFellingZone() {
            _fellingZone.InitTrees();
        }
    }
}