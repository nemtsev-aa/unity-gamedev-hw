using Game.GameEngine.GameResources;
using UnityEngine;

namespace FarmingSystem {

    [CreateAssetMenu(
        fileName = nameof(ResourceConfig),
        menuName = "ResourceConfigs/" + nameof(ResourceConfig)
    )]
    public class ResourceConfig : ScriptableObject {
        [field: SerializeField] public ResourceType Type { get; private set; }
        [field: SerializeField] public Sprite IconToUI { get; private set; }
        [field: SerializeField] public float RespawnTime { get; private set; }
    }
}



