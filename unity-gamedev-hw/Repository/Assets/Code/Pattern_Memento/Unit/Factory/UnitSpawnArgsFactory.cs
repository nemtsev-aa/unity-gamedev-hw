using GameEngine;
using Pattern_Memento;
using UnityEngine;

namespace Unit_Spawn_System {
    public sealed partial class UnitSpawnArgsFactory {
        private readonly UnitPrefabConfigs _prefabConfigs;
        private readonly Transform _gameField;

        public UnitSpawnArgsFactory(UnitPrefabConfigs prefabConfigs, Transform gameField) {
            _prefabConfigs = prefabConfigs;
            _gameField = gameField;
        }

        public UnitSpawnArgs Get(UnitData data = null) {
            UnitPrefabInfo info;
            Unit prefab;
            Vector3 position;
            Quaternion rotation;

            if (data == null) {
                info = _prefabConfigs.GetRandomUnitPrefabInfo();

                prefab = info.Prefab;

                position = GetRandomPositionInsideTransform(_gameField);
                rotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));

                return new UnitSpawnArgs(prefab, position, rotation);
            }

            info = _prefabConfigs.GetUnitPrefabByType(data.Type);
            prefab = info.Prefab;

            position = data.Position;
            rotation = Quaternion.Euler(data.Rotation);

            return new UnitSpawnArgs(prefab, position, rotation);
        }


        private Vector3 GetRandomPositionInsideTransform(Transform area) {
            Vector3 scale = area.localScale;

            return new Vector3(
                Random.Range(-scale.x / 2, scale.x / 2) + area.position.x,
                0,
                Random.Range(-scale.z / 2, scale.z / 2) + area.position.y
            );
        }
    }
}
