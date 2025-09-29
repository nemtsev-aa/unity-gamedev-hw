using Client.Components.Movement;
using Client.Components.Targeting;
using Client.Components.Teams;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Systems.Spatial {

    public sealed class SpatialTargetingGridSystem : IEcsRunSystem, IEcsInitSystem {
        public const float CELL_SIZE = 8f;

        private readonly EcsFilterInject<Inc<Position, Team, TargetableComponent>> _targetableFilter;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<TargetableComponent> _targetablePool;

        private readonly Dictionary<Vector3Int, GridCell> _grid = new();
        private readonly Dictionary<int, Vector3Int> _entityToCell = new();

        private EcsWorld _world;

        public void Init(IEcsSystems systems) {
            _world = systems.GetWorld();
            _grid.Clear();
            _entityToCell.Clear();
        }

        public void Run(IEcsSystems systems) {

            if (_targetableFilter.Value == null)
                return;

            UpdateGridPositions();
            CleanEmptyCells();
        }

        private void UpdateGridPositions() {
            foreach (int entity in _targetableFilter.Value) {
                if (!_world.IsEntityAlive(entity)) continue;

                if (!_targetablePool.Value.Has(entity) || !_positionPool.Value.Has(entity)) {
                    RemoveEntity(entity);
                    continue;
                }

                var targetable = _targetablePool.Value.Get(entity);
                if (!targetable.IsActive) {
                    RemoveEntity(entity);
                    continue;
                }

                ref var position = ref _positionPool.Value.Get(entity);
                var newCell = PositionToCellKey(position.Value);

                if (_entityToCell.TryGetValue(entity, out var currentCell)) {
                    if (currentCell != newCell) {
                        MoveEntity(entity, currentCell, newCell);
                    }
                } else {
                    AddEntity(entity, newCell);
                }
            }
        }


        private Vector3Int PositionToCellKey(Vector3 position) {
            return new Vector3Int(
                Mathf.FloorToInt(position.x / CELL_SIZE),
                Mathf.FloorToInt(position.y / CELL_SIZE),
                Mathf.FloorToInt(position.z / CELL_SIZE)
            );
        }

        private void AddEntity(int entity, Vector3Int cellKey) {
            if (!_grid.TryGetValue(cellKey, out var cell)) {
                cell = new GridCell();
                _grid[cellKey] = cell;
            }

            cell.Entities.Add(entity);
            _entityToCell[entity] = cellKey;
        }

        private void MoveEntity(int entity, Vector3Int fromCell, Vector3Int toCell) {
            if (_grid.TryGetValue(fromCell, out var oldCell)) {
                oldCell.Entities.Remove(entity);
            }

            AddEntity(entity, toCell);
        }

        public void RemoveEntity(int entity) {
            
            if (_entityToCell.TryGetValue(entity, out var cellKey)) {
                
                if (_grid.TryGetValue(cellKey, out var cell)) {
                    cell.Entities.Remove(entity);
                }
                _entityToCell.Remove(entity);
            }
        }

        private void CleanEmptyCells() {
            var cellsToRemove = new List<Vector3Int>();

            foreach (var kvp in _grid) {
                if (kvp.Value.Entities.Count == 0) {
                    cellsToRemove.Add(kvp.Key);
                }
            }

            foreach (var cellKey in cellsToRemove) {
                _grid.Remove(cellKey);
            }
        }

        public IEnumerable<int> GetPotentialTargetsInRange(Vector3 center, float range, Team seekingTeam) {
            if (_grid == null) yield break;

            var centerCell = PositionToCellKey(center);
            var radiusInCells = Mathf.CeilToInt(range / CELL_SIZE);
            var processedEntities = new HashSet<int>();

            for (int x = -radiusInCells; x <= radiusInCells; x++) {
                for (int y = -radiusInCells; y <= radiusInCells; y++) {
                    for (int z = -radiusInCells; z <= radiusInCells; z++) {
                        var cellKey = new Vector3Int(
                            centerCell.x + x,
                            centerCell.y + y,
                            centerCell.z + z
                        );

                        if (_grid.TryGetValue(cellKey, out var cell)) {
                            foreach (var entity in cell.Entities) {
                                if (processedEntities.Contains(entity)) continue;
                                if (!_world.IsEntityAlive(entity)) continue;

                                if (IsValidTarget(entity, seekingTeam)) {
                                    processedEntities.Add(entity);
                                    yield return entity;
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsValidTarget(int entity, Team seekingTeam) {
            if (!_world.IsEntityAlive(entity)) return false;
            if (!_targetablePool.Value.Has(entity) || !_teamPool.Value.Has(entity)) return false;

            var targetable = _targetablePool.Value.Get(entity);
            var targetTeam = _teamPool.Value.Get(entity);

            return targetable.IsActive && targetTeam.Value != seekingTeam.Value;
        }

        public class GridCell {
            public readonly HashSet<int> Entities = new HashSet<int>();
            public int Count => Entities.Count;
        }

        // Публичные методы для визуализации
        public int GetCellCount() {
            return _grid.Count;
        }

        public int GetEntityCount() {
            return _entityToCell.Count;
        }

        public Dictionary<Vector3Int, int> GetCellPopulation() {
            var result = new Dictionary<Vector3Int, int>();
            foreach (var kvp in _grid) {
                result[kvp.Key] = kvp.Value.Entities.Count;
            }
            return result;
        }

        public Vector3? GetEntityPosition(int entityId) {
            if (_positionPool.Value.Has(entityId)) {
                return _positionPool.Value.Get(entityId).Value;
            }
            return null;
        }
    }
}

