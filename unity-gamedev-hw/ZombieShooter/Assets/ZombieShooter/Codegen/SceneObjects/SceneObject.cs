using UnityEngine;
using Atomic.Entities;

namespace ZombieShooter.SceneObjects {

    public abstract class SceneObject : MonoBehaviour {
        [field: SerializeField] public SceneEntity Entity { get; private set; }
    }
}
