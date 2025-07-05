using System;
using System.Collections.Generic;
using UnityEngine;

namespace EquipmentSystem.Core {

    [CreateAssetMenu(
        fileName = nameof(EquipmentServiceConfig),
        menuName = "EquipmentSystem/new " + nameof(EquipmentServiceConfig)
    )]
    public sealed class EquipmentServiceConfig : ScriptableObject {
        [field: SerializeField] public List<EquipmentSlotConfig> SlotsConfig { get; private set; }

        private void OnValidate() {

            if (SlotsConfig.Count == 0)
                throw new ArgumentNullException($"EquipmentServiceConfig is empty!");
        }
    }
}
