using Client.Components.Common;
using Client.Components.Teams;
using Client.Installer;
using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using System;
using System.Collections.Generic;
using UnitPoolSystem;
using UnityEngine;
using Zenject;

namespace GameCycleSystem {

    public sealed class UnitManager : IGameStartListener,
                                      IGameFinishListener,
                                      IGamePauseListener {

        public bool IsActive { get; private set; }
        public bool IsPause { get; private set; }

        private readonly UnitBaseConfigs _config;
        private readonly UnitSpawnPointPresenter _spawnPointPresenter;
        private readonly UnitManagerConfig _managerConfig;
        private readonly HashSet<UnitEntity> _activeUnits = new();

        private UnitPool _archerPool;
        private UnitPool _swordsManPool;
        private EcsWorld _world;
        private EntityManager _entityManager;

        public UnitManager(UnitManagerConfig managerConfig,
                           UnitBaseConfigs config,
                           UnitSpawnPointPresenter spawnPointPresenter,
                           [Inject(Id = "ArcherPool")] UnitPool pool1,
                           [Inject(Id = "SwordsManPool")] UnitPool pool2) {

            _managerConfig = managerConfig;
            _config = config;
            _spawnPointPresenter = spawnPointPresenter;
            _archerPool = pool1;
            _swordsManPool = pool2;
        }

        public void OnStartGame() {
            _world = EcsStartup.Instance.DefaultWorld;
            _entityManager = EcsStartup.Instance.EntityManager;

            IsActive = true;

            if (_managerConfig.GetArgsByType(UnitTypes.Archer, out UnitPoolArgs args1) == true)
                _archerPool.Create(args1.Type, args1.Size);

            if (_managerConfig.GetArgsByType(UnitTypes.Swordsman, out UnitPoolArgs args2) == true)
                _swordsManPool.Create(args2.Type, args2.Size);
        }

        public void OnPauseGame() {
            IsPause = !IsPause;
        }

        public void OnFinishGame() {
            IsActive = false;

            foreach (UnitEntity iUnit in _activeUnits) {

                if (iUnit.Type == UnitTypes.Archer)
                    _archerPool.ReturnUnitToPool(iUnit);
                else
                    _swordsManPool.ReturnUnitToPool(iUnit);

                iUnit.Destroyed -= OnDestroyed;
            }

            _activeUnits.Clear();
            _archerPool.Clear();
            _swordsManPool.Clear();
        }

        public void CreateUnit(TeamTypes team, UnitTypes unitType) {

            UnitPool currentPool;
            Transform spawnTransform;

            if (unitType == UnitTypes.Archer)
                currentPool = _archerPool;
            else
                currentPool = _swordsManPool;

            spawnTransform = _spawnPointPresenter.GetSpawnPointByTeamType(team).Transform;

            bool _isUnitEntity = currentPool.TryGetUnitFromPool(unitType, out UnitEntity unitEntity);

            if (_isUnitEntity == true) {

                if (_config.TryGetConfigByType(unitType, out UnitBaseConfig config) == true) {
                    unitEntity.Init(team, config);
                    unitEntity.Destroyed += OnDestroyed;

                    unitEntity.transform.position = spawnTransform.position;
                    unitEntity.transform.rotation = spawnTransform.rotation;

                    unitEntity.Initialize(_world);

                    _entityManager.Add(unitEntity);
                    _activeUnits.Add(unitEntity);

                    return;
                }

                throw new ArgumentException($"UnitType {unitType} not found in UnitBaseConfigs!");
            }

            throw new ArgumentException($"Create Unit {team} {unitType} failed!");
        }

        private void OnDestroyed(UnitEntity unit) {
            unit.Destroyed -= OnDestroyed;

            if (_activeUnits.Remove(unit) == true) {

                if (unit.Type == UnitTypes.Archer)
                    _archerPool.ReturnUnitToPool(unit);
                else
                    _swordsManPool.ReturnUnitToPool(unit);
            }
        }
    }
}
