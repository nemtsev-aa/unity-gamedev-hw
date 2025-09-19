using Zenject;
using UnityEngine;
using FarmingSystem;
using Conveyors.Entity;
using UnityEngine.SceneManagement;

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

        void Update() {

            if (Input.GetKeyDown(KeyCode.R) == true) 
                ReloadCurrentScene();
        }

        public void ReloadCurrentScene() {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}