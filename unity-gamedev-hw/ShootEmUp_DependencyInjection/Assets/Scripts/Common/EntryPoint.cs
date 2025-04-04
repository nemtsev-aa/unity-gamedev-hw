using UnityEngine;
using Zenject;

namespace ShootEmUp {
    public sealed class EntryPoint : MonoBehaviour {
        [SerializeField] private UIManager _uIManager;

        private GameMediator _gameMediator;

        [Inject]
        public void Construct(GameMediator gameMediator) {
            _gameMediator = gameMediator;
        }

        private void Start() {
            _uIManager.Init();
            _gameMediator.Init(_uIManager);
        }
    }
}


