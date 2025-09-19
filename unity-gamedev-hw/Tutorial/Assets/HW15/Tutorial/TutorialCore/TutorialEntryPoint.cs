using Zenject;
using UnityEngine;
using FarmingSystem;
using UnityEngine.SceneManagement;
using System;
using BehaviorTree.PlayerCoreSubsystem;
using Cysharp.Threading.Tasks;
using Tutorial.PlayerUpgrades;
using UpgradesSystem.Core;
using ProgressService;

namespace Tutorial.Core {

    public sealed class TutorialEntryPoint : MonoBehaviour, IDisposable {
        private TutorialStateRunner _runner;
        private TutorialState _tutorialState;
        private FellingZone _fellingZone;
        private PlayerProvider _playerProvider;
        private PlayerUpgradeSystem _playerUpgrades;
        private MoneyChangeObserver _moneyChangeObserver;

        [Inject]
        private void Construct(TutorialStateRunner runner,
                               TutorialState tutorialState,
                               FellingZone fellingZone,
                               PlayerProvider playerProvider,
                               IUpgradeSystem upgradeSystem,
                               MoneyChangeObserver moneyChangeObserver) {

            _runner = runner;
            _tutorialState = tutorialState;
            _fellingZone = fellingZone;
            _playerProvider = playerProvider;
            _playerUpgrades = (PlayerUpgradeSystem)upgradeSystem;
            _moneyChangeObserver = moneyChangeObserver;
        }

        private void Start() {
            WaitAndInitialize().Forget();

            _tutorialState.Completed += TutorialState_Completed;

            _runner.Start();
            _fellingZone.InitTrees();
        }

        private void Update() => _runner.Update();
        
        private async UniTask WaitAndInitialize() {
            try {
                await _playerProvider.InitializeAsync();

                if (_playerProvider.ProgressData != null) {
                    var data = _playerProvider.ProgressData;
                    _playerUpgrades.Init(data);
                    _moneyChangeObserver.Init(data);
                }
            }
            catch (Exception ex) {
                Debug.LogError($"Failed to initialize GameplayMediator: {ex}");
            }
        }

        private void TutorialState_Completed() {
            _playerProvider.ResetProgress();
            ReloadCurrentScene();
        }

        private void ReloadCurrentScene() {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void Dispose() {
            _tutorialState.Completed += TutorialState_Completed;
        }
    }
}