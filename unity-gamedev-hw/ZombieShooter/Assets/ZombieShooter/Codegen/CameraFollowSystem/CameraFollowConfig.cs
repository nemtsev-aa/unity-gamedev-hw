using UnityEngine;

namespace AtomicFramework.CameraFollowSystem {

    [CreateAssetMenu(
        fileName = nameof(CameraFollowConfig),
        menuName = "Configs/" + nameof(CameraFollowConfig)
    )]

    public sealed class CameraFollowConfig : ScriptableObject {
        [field: SerializeField] public Vector3 Offset { get; private set; }
        [field: SerializeField] public float SmoothSpeed { get; private set; } = 5f;
        [field: SerializeField] public bool LookAtTarget { get; private set; } = true;
        [field: SerializeField] public float RotationSmoothness { get; private set; } = 10f;
        [field: SerializeField] public bool UseFixedUpdate { get; private set; } = false;
        [field: SerializeField] public float DistanceThreshold { get; private set; } = 0.1f;
    }
}


