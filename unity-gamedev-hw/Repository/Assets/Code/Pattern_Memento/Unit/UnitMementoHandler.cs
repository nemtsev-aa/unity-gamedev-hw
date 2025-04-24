using System;
using GameEngine;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Unit_Spawn_System;

namespace Pattern_Memento {
    public sealed class UnitMementoHandler : IMementoHandler {
        private readonly UnitManager _unitManager;
        private readonly UnitSpawnArgsFactory _factory;

        private List<Unit> _units;
        private List<Unit> _restoreUnits;

        public UnitMementoHandler(UnitManager unitManager, UnitSpawnArgsFactory factory) {
            _unitManager = unitManager;
            _factory = factory;
        }

        public IMementos SaveState(string name = "") {

            string mementoID = name == "" ? DateTime.Now.ToString("hhmmss") : name;

            _units = _unitManager.GetAllUnits().ToList();

            if (_units.Count() == 0)
                throw new ArgumentNullException($"Unit List is empty!");

            if (TryCreateMementoList(mementoID, out List <UnitMemento> list) == true) {
                Debug.Log($"UnitMementosManager: SaveState success! The number of units: {list.Count()}");
                //PrintStatistic(mementolist);
                return new UnitMementos(mementoID, list);
            }

            throw new ArgumentException($"UnitMementosManager: SaveState failed!");
        }

        public void RestoreState(IMementos memento) {

            if (_units != null && _units.Count > 0) 
                _units.Clear();
            else
                _units = new();

            _units.AddRange(_unitManager.GetAllUnits().ToList());

            _restoreUnits = new List<Unit>();
            UnitMementos mementos = (UnitMementos)memento;

            foreach (UnitMemento iMemento in mementos.Mementos) {
                UnitData data = iMemento.Data;

                if (CheckAvailabilityUnitOnMap(data.ID, out Unit unit)) {

                    SynchronizationUnitData(data, unit);

                    _units.Remove(unit);
                    continue;
                }

                RestoreUnit(iMemento);
            }

            RemoveUnnecessaryUnits();

            Debug.Log($"The state of the [Units] has been restored based on the memento: {memento.ID}");
        }

        private bool TryCreateMementoList(string mementoID, out List<UnitMemento> list) {
            try {
                list = new();

                for (int i = 0; i < _units.Count(); i++) {
                    Unit iUnit = _units.ElementAt(i);

                    string id = $"{iUnit.Type} ({i})";
                    iUnit.gameObject.name = id;

                    UnitData data = new UnitData(
                        id,
                        iUnit.Type,
                        iUnit.HitPoints,
                        iUnit.Position,
                        iUnit.Rotation
                    );

                    UnitMemento unitMemento = new UnitMemento(mementoID, data);

                    list.Add(unitMemento);
                }

                return true;
            }
            catch (Exception e) {
                Debug.LogError($"CreateMementoList failed: {e.Message}");

                list = null;
                return false;
            }
        }

        private void SynchronizationUnitData(UnitData data, Unit unit) {
            if (CheckTransformValues(data, unit) == false)
                ReplaceUnit(unit, data);

            if (CheckHitPointsValue(data, unit) == false)
                UpdateHitPoints(data, unit);
        }

        private void RestoreUnit(UnitMemento memento) {
            UnitData data = memento.Data;
            UnitSpawnArgs spawnParameters = _factory.Get(data);

            Unit newUnit = _unitManager.SpawnUnit(spawnParameters.Prefab, spawnParameters.Position, spawnParameters.Rotation);
            UpdateHitPoints(data, newUnit);
            //string newUnitName = $"{newUnit.Type} ({_unitManager.GetAllUnits().Count()})";
            string newUnitName = data.ID;
            newUnit.gameObject.name = newUnitName;

            Debug.Log($"Restore Unit: {newUnitName}");
        }

        private bool CheckAvailabilityUnitOnMap(string id, out Unit unit) {

            foreach (Unit iUnit in _units) {

                if (iUnit.gameObject.name == id) {
                    unit = iUnit;
                    return true;
                }
            }

            unit = null;
            return false;
        }

        private bool CheckTransformValues(UnitData data, Unit unit) {

            if (data.Position != unit.Position)
                return false;

            if (data.Rotation != unit.Rotation)
                return false;

            return true;
        }

        private bool CheckHitPointsValue(UnitData data, Unit unit) {
            if (data.HitPoints != unit.HitPoints)
                return false;

            return true;
        }

        private void UpdateHitPoints(UnitData data, Unit unit) {
            unit.HitPoints = data.HitPoints;
        }

        private void ReplaceUnit(Unit unit, UnitData data) {
            _unitManager.DestroyUnit(unit);

            UnitSpawnArgs spawnParameters = _factory.Get(data);
            Unit restoreUnit = _unitManager.SpawnUnit(spawnParameters.Prefab, spawnParameters.Position, spawnParameters.Rotation);
            restoreUnit.gameObject.name = data.ID;

            _restoreUnits.Add(restoreUnit);
        }

        private void RemoveUnnecessaryUnits() {    
            foreach (var iUnit in _units) {
                _unitManager.DestroyUnit(iUnit);
            }

            _units.Clear();
        }

        private void PrintStatistic(List<UnitMemento> mementolist) {
            foreach (var iUnitMemento in mementolist) {
                Debug.Log($"UnitID: {iUnitMemento.Data.ID}," +
                         $" UnitPosition {iUnitMemento.Data.Position}," +
                         $" UnitRotation {iUnitMemento.Data.Rotation}");
            }
        }
    }
}

