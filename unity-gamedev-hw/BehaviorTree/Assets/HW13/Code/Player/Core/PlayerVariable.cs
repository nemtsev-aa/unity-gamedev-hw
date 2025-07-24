using MBT;
using System;
using BehaviorTree.PlayerCoreSubsystem;

namespace BehaviorTree.Brain {

    [Serializable]
    public class PlayerVariable : Variable<Player> {

        protected override bool ValueEquals(Player val1, Player val2) {
            return val1 == val2;
        }
    }
}



