using System.Collections.Generic;
using UnityEngine;

namespace PresentationModel {
    [CreateAssetMenu(
       fileName = nameof(DataConfigs),
       menuName = "Configs/" + nameof(DataConfigs))
    ]

    public sealed class DataConfigs : ScriptableObject {
        [SerializeField] private List<DataConfig> _configs;

        public IReadOnlyList<DataConfig> Configs => _configs;
    }
}


