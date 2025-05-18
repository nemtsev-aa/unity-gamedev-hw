using System;
using UnityEngine;

namespace AtomicFramework.EnemySystem {

    [Serializable]
    public class ContainersPresenter {
        [field: SerializeField] public Transform WorldContainer { get; private set; }
        [field: SerializeField] public Transform BulletContainer { get; private set; }
        [field: SerializeField] public Transform EnemyContainer { get; private set; }
        [field: SerializeField] public Transform PointerIconContainer { get; private set; }
    }
}