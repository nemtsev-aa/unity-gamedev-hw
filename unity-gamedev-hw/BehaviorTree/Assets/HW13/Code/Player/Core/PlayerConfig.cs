using UnityEngine;

namespace BehaviorTree.PlayerCoreSubsystem {
    [CreateAssetMenu(
        fileName = nameof(PlayerConfig),
        menuName = "Configs/PlayerConfig/" + nameof(PlayerConfig)
    )]
    public sealed class PlayerConfig : ScriptableObject {
        [field: SerializeField] public float MoveSteed { get; private set; }
        [field: SerializeField] public int InventoryMaxAmount { get; private set; }

        [field: SerializeField] public float DistanceToCollect { get; private set; } = 5f;
        [field: SerializeField] public float CollectionSpeed { get; private set; } = 0.3f;
        [field: SerializeField] public LayerMask ResourceMask { get; private set; }
        [field: SerializeField] public LayerMask LootMask { get; private set; }

        [field: SerializeField] public float FellingDuration { get; private set; } = 1.2f;
    }
}