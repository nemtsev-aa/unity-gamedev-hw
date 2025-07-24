using System;
using MBT;

namespace BehaviorTree.Brain {

    [Serializable]
    public class BotBrainDataVariable : Variable<BotBrainData> {

        protected override bool ValueEquals(BotBrainData val1, BotBrainData val2) {
            return val1 == val2;
        }
    }

    [Serializable]
    public class BotBrainDataReference : VariableReference<BotBrainDataVariable, BotBrainData> {
  
        public BotBrainDataReference(VarRefMode mode = VarRefMode.EnableConstant) {
            SetMode(mode);
        }

        public BotBrainDataReference(BotBrainData defaultConstant) {
            useConstant = true;
            constantValue = defaultConstant;
        }

        public BotBrainData Value {
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



