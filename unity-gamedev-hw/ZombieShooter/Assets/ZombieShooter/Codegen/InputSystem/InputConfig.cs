using UnityEngine;

namespace AtomicFramework.InputSystem {

    [CreateAssetMenu(
        fileName = nameof(InputConfig),
        menuName = "Configs/" + nameof(InputConfig)
    )]

    public sealed class InputConfig : ScriptableObject {
        [field: SerializeField] public KeyCode Forward { get; private set; }
        [field: SerializeField] public KeyCode Back { get; private set; }
        [field: SerializeField] public KeyCode Left { get; private set; }
        [field: SerializeField] public KeyCode Right { get; private set; }
        [field: SerializeField] public int MouseButton { get; private set; }
    }
}


