using UnityEngine;

namespace UI.Components.Screens {

    [CreateAssetMenu(
        fileName = nameof(UIScreenConfigs),
        menuName = "Configs/" + nameof(UIScreenConfigs)
    )]
    public sealed class UIScreenConfigs : ScriptableObject {
        [field: SerializeField] public UIScreenConfig[] Configs { get; private set; }
    }
}
