using UnityEngine;
using Zenject;

namespace ShootEmUp {
    public sealed class EntryPoint : MonoBehaviour {
        [SerializeField] private GameCycle _gameCycle;
        [SerializeField] private UIManager _uIManager;

        private GameMediator _gameMediator;
        private GameCycleInstaller _gameCycleInstaller;

        [Inject]
        public void Construct(GameCycleInstaller gameCycleInstaller, GameMediator gameMediator) {
            _gameCycleInstaller = gameCycleInstaller;
            _gameMediator = gameMediator;
        }

        private void Start() {
            _uIManager.Init();
            _gameMediator.SetManagers(_gameCycle, _uIManager);

            _gameCycleInstaller.SetGameCycle(_gameCycle);
            _gameCycleInstaller.AddGameListeners();
        }
    }
}


