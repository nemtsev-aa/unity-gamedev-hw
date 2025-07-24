using System;
using UnityEngine;

namespace Game.GameEngine.GameResources {

    [Serializable]
    public sealed class ResourceInfo {
        [field: SerializeField] public ResourceType Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}