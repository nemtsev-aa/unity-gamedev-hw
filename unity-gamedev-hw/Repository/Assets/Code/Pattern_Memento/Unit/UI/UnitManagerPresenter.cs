using GameEngine;
using R3;
using System;
using System.Linq;
using UnityEngine;

namespace Unit_Spawn_System {
    public sealed class UnitManagerPresenter : IDisposable {
        private readonly UnitManager _model;
        private readonly UnitManagerView _view;
        private readonly UnitSpawnArgsFactory _factory;
        private readonly CompositeDisposable _compositeDisposable = new();

        public UnitManagerPresenter(UnitManager model, UnitManagerView view, UnitSpawnArgsFactory factory) {
            _model = model;
            _view = view;
            _factory = factory;

            CreateReactiveSubscribes();
        }

        private void CreateReactiveSubscribes() {
            _view.OnAddButtonClicked
                .Subscribe(OnAddButtonClick)
                .AddTo(_compositeDisposable);

            _view.OnRemoveButtonClicked
                .Subscribe(OnRemoveButtonClick)
                .AddTo(_compositeDisposable);

            _view.OnDamageButtonClicked
                .Subscribe(OnDamageButtonClick)
                .AddTo(_compositeDisposable);
        }

        private void OnAddButtonClick(R3.Unit unit) {
            UnitSpawnArgs spawnParameters = _factory.Get();

            GameEngine.Unit newUnit = _model.SpawnUnit(spawnParameters.Prefab, spawnParameters.Position, spawnParameters.Rotation);
            string newUnitName = $"{newUnit.Type} ({_model.GetAllUnits().Count()})";
            newUnit.gameObject.name = newUnitName;

            Debug.Log($"Add new Unit: {newUnitName}");
        }

        private void OnRemoveButtonClick(R3.Unit unit) {
            var randomUnit = GetRandomUnit();
            _model.DestroyUnit(randomUnit);

            Debug.Log($"Remove Unit: {randomUnit.gameObject.name} {randomUnit.Type}");
        }

        private void OnDamageButtonClick(R3.Unit unit) {
            var randomUnit = GetRandomUnit();
            randomUnit.HitPoints--;

            Debug.Log($"UnitHitPoints changed: {randomUnit.gameObject.name} {randomUnit.Type} {randomUnit.HitPoints}");
        }

        private GameEngine.Unit GetRandomUnit() {
            var units = _model.GetAllUnits();
            int randomIndex = UnityEngine.Random.Range(0, units.Count());

            return units.ElementAt(randomIndex);
        }

        public void Dispose() {

            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}
