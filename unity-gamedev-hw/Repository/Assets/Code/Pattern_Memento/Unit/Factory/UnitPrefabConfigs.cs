using GameEngine;
using System.Collections.Generic;
using UnityEngine;

namespace Unit_Spawn_System {
    [CreateAssetMenu(
        fileName = nameof(UnitPrefabConfigs),
        menuName = "Configs/" + nameof(UnitPrefabConfigs)
    )]

    public sealed class UnitPrefabConfigs : ScriptableObject {
        [SerializeField] public List<UnitPrefabInfo> _prefabsInfo;

        public UnitPrefabInfo GetUnitPrefabByType(string type) {

            foreach (var iInfo in _prefabsInfo) {

                if (iInfo.Type.ToString() == type)
                    return iInfo;
            }

            return new UnitPrefabInfo();
        }

        public UnitPrefabInfo GetRandomUnitPrefabInfo() {
            return _prefabsInfo[Random.Range(0, _prefabsInfo.Count)];
        }
    }
}

