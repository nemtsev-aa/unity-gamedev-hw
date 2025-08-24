using System;
using UnityEngine;

namespace SceneManagementSystem {

    [Serializable]
    public sealed class SceneReferencesProvider {
        [field: SerializeField] public string MenuSceneRef { get; private set; }
        [field: SerializeField] public string GameSceneRef { get; private set; }
    }
}
