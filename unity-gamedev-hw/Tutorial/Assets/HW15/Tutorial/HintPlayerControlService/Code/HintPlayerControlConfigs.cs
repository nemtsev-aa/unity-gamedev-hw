using UnityEngine;
using System.Collections.Generic;

namespace HintPlayerControlService {

    [CreateAssetMenu(
        fileName = nameof(HintPlayerControlConfigs),
        menuName = "HintPlayerControlServce/Config/" + nameof(HintPlayerControlConfigs)
    )]
    public sealed class HintPlayerControlConfigs : ScriptableObject {
        [field: SerializeField] public List<HintPlayerControlConfig> Configs { get; private set; }
    }
}