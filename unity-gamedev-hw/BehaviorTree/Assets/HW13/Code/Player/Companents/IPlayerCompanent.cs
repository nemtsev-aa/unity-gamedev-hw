using UnityEngine;
using BehaviorTree.PlayerCoreSubsystem;

namespace BehaviorTree.PlayerCompanents {

    public interface IPlayerCompanent {
        Transform Root { get; }
        bool IsActive { get; }
        void Init(Transform root, PlayerConfig config);
        void Activate(bool status);
    }
}



