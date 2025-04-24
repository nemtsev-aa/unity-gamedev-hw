using GameEngine;
using Pattern_Memento;
using SaveLoadSystem.Core;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SaveLoadSystem {
    public sealed class EntryPoint : MonoBehaviour {
        [SerializeField] private bool _saveAfterStart;

        private UnitManager _unitManager;
        private ResourceService _resourceService;

        private MementoCoordinator _coordinator;
        private Dictionary<MementoTypes, MementoServices> _services;
        private SaveLoadPopupController _saveLoadController;

        private SaveLoadManager _saveLoadManager;
        private SaveLoadMediator _saveLoadMediator;
        private SaveLoadPopup _saveLoadPopup;
        private CommonPopup _commonPopup;

        [Inject]
        public void Construct(
            UnitManager unitManager,
            ResourceService resourceService,
            MementoCoordinator coordinator,
            SaveLoadManager saveLoadManager,
            SaveLoadPopup saveLoadPopup,
            CommonPopup commonPopup) {

            _unitManager = unitManager;
            _resourceService = resourceService;
            _coordinator = coordinator;
            _saveLoadManager = saveLoadManager;
            _saveLoadPopup = saveLoadPopup;
            _commonPopup = commonPopup;
        }

        private void Start() {
            Init();
        }

        private void Init() {
            _services = _coordinator.Services;

            InitUnitMementoCompanents();
            InitResourceMementoCompanents();
            InitSaveLoadSystemCompanents();

            if (_saveAfterStart == true)
                _coordinator.CreateMementos();

            _commonPopup.Show(true);
        }

        private void InitUnitMementoCompanents() {
            IEnumerable<Unit> units = FindObjectsOfTypeIncludingDisabled<Unit>();
            _unitManager.SetupUnits(units);
        }

        private void InitResourceMementoCompanents() {
            IEnumerable<Resource> resources = FindObjectsOfTypeIncludingDisabled<Resource>();
            _resourceService.SetResources(resources);
        }

        private void InitSaveLoadSystemCompanents() {
            _saveLoadController = new SaveLoadPopupController(_coordinator, _saveLoadPopup);
            _saveLoadMediator = new SaveLoadMediator(_saveLoadManager, _coordinator, _saveLoadPopup);
        }

        private T[] FindObjectsOfTypeIncludingDisabled<T>() {
            var ActiveScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            var RootObjects = ActiveScene.GetRootGameObjects();
            var MatchObjects = new List<T>();

            foreach (var ro in RootObjects) {
                var Matches = ro.GetComponentsInChildren<T>(true);
                MatchObjects.AddRange(Matches);
            }

            return MatchObjects.ToArray();
        }
    }
}

