using UnityEngine;
using System;

namespace ShootEmUp {
    [Serializable]
    public class ContainersPresenter {
        [field: SerializeField] public Transform WorldContainer { get; private set; }
        [field: SerializeField] public Transform BulletContainer { get; private set; }
        [field: SerializeField] public Transform EnemyContainer { get; private set; }
    }
}
