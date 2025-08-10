using System;

namespace BehaviorTree.PlayerCompanents {

    public interface IUpdatedPlayerCompanent : IPlayerCompanent, IDisposable {
        bool IsCooldown { get; }
        void Update(float deltaTime);
    }
}



