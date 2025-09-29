using Client.Components;
using System;
using UnityEngine;
using System.Collections.Generic;
using Client.Components.Common;

namespace UnitPoolSystem {

    [Serializable]
    public sealed class UnitPrefabPresenter {

        [field: SerializeField] public List<UnitEntity> Prefabs { get; private set; }

        public bool TryGetUnitEntityByType(UnitTypes type, out UnitEntity unitEntity) {

            for (int i = 0; i < Prefabs.Count; i++) {
                var iUnit = Prefabs[i];

                if (iUnit.Type == type) {
                    unitEntity = iUnit;

                    return true;
                }
            }

            unitEntity = null;
            return false;
        }
    }
}