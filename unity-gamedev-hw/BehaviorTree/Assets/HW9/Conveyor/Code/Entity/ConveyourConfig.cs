using UnityEngine;
using Game.GameEngine;
using Game.GameEngine.GameResources;

namespace Conveyors.Entity {

    [CreateAssetMenu(
        fileName = nameof(ConveyourConfig),
        menuName = "Conveyors/" + nameof(ConveyourConfig)
    )]
    public sealed class ConveyourConfig : ScriptableObject {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public ObjectType ObjectType { get; private set; } = ObjectType.CONVEYOR;

        [Header("Load Zone")]
        [field: SerializeField] public ResourceType InputResourceType { get; private set; }
        [field: SerializeField] public int InputCapacity { get; private set; }

        [Header("Unload Zone")]
        [field: SerializeField] public ResourceType OutputResourceType { get; private set; }
        [field: SerializeField] public int OutputCapacity { get; private set; }

        [Header("Work")]
        [field: SerializeField] public float WorkTime { get; private set; }
    }
}