using Zenject;
using UnityEngine;

namespace GameCycleSystem {

    public sealed class EntryPoint : MonoBehaviour {
        private GameMediator _gameMediator;

        [Inject]
        public void Construct(GameMediator gameMediator) {
            _gameMediator = gameMediator;
        }

        private void Start() =>
            _gameMediator.Init();
    }
}