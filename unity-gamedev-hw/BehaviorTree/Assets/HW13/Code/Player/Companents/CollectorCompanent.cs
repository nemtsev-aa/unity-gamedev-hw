using System;
using UnityEditor;
using UnityEngine;
using FarmingSystem;
using BehaviorTree.PlayerCoreSubsystem;
using static Unity.VisualScripting.Member;

namespace BehaviorTree.PlayerCompanents {

    [Serializable]
    public class CollectorCompanent : IPlayerCompanent {
        public event Action<Loot> LootCollected;

        public Transform Root { get; private set; }
        public bool IsActive { get; private set; } = false;

        public float DistanceToCollect { get; private set; }
        public float CollectionSpeed { get; private set; }

        public LayerMask ResourceMask { get; private set; }
        public LayerMask LootMask { get; private set; }

        public void Init(Transform root, PlayerConfig config) {
            Root = root;

            DistanceToCollect = config.DistanceToCollect;
            CollectionSpeed = config.CollectionSpeed;

            ResourceMask = config.ResourceMask;
            LootMask = config.LootMask;
        }

        public void Activate(bool status) {
            IsActive = status;
        }

        public bool TryCollect(Loot loot) {

            if (IsActive == false || loot == null)
                return false;

            loot.Collect(Root.transform.position, CollectionSpeed);
            LootCollected?.Invoke(loot);

            return true;
        }
    }
}



