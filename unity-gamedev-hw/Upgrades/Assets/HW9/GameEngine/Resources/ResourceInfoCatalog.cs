using System;
using UnityEngine;

namespace Game.GameEngine.GameResources {

    [CreateAssetMenu(
        fileName = nameof(ResourceInfoCatalog),
        menuName = "GameEngine/GameResources/New " + nameof(ResourceInfoCatalog)
    )]
    public sealed class ResourceInfoCatalog : ScriptableObject {
        [field: SerializeField] public ResourceInfo[] Configs { get; private set; }

        public ResourceInfo FindResource(ResourceType type) {
            for (int i = 0, count = Configs.Length; i < count; i++) {
                var info = Configs[i];
                
                if (info.Type == type) 
                    return info;
            }

            throw new Exception($"Resource {type} is not found!");
        }
    }
}