using UnityEngine;

namespace AtomicFramework.EnemySystem {

    public sealed class EnemyPositions : MonoBehaviour {
        [SerializeField] private Transform[] _spawnPositions;

        public Transform RandomSpawnPosition() {
            return RandomTransform(_spawnPositions);
        }

        private Transform RandomTransform(Transform[] transforms) {
            return transforms[Random.Range(0, transforms.Length)];
        }
    }
}