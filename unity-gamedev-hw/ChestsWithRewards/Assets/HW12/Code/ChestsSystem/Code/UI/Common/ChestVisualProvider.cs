using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestsSystem {

    [Serializable]
    public sealed class ChestVisualProvider {
        [SerializeField] private List<ChestVisual> _visuals;

        public ChestVisual GetModelByType(ChestType type) {

            for (int i = 0; i < _visuals.Count; i++) {
                var iModel = _visuals[i];

                if (iModel.Type == type)
                    return iModel;
            }

            throw new ArgumentException($"Invalid ChestType {type}");
        }
    }
}
