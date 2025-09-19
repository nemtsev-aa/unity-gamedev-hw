using System;
using BehaviorTree.PlayerCoreSubsystem;
using MBT;

namespace BehaviorTree.Brain {
    [Serializable]
    public class PlayerReference : VariableReference<PlayerVariable, Player> {

        public PlayerReference(VarRefMode mode = VarRefMode.EnableConstant) {
            SetMode(mode);
        }

        public PlayerReference(Player defaultConstant) {
            useConstant = true;
            constantValue = defaultConstant;
        }

        public Player Value {
            get {
                return (useConstant) ? constantValue : this.GetVariable().Value;
            }
            set {
                if (useConstant) {
                    constantValue = value;
                } else {
                    this.GetVariable().Value = value;
                }
            }
        }
    }
}



