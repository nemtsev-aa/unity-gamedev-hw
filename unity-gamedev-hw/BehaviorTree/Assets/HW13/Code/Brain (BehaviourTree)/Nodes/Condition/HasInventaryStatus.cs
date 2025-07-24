using MBT;
using Zenject;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(HasInventaryStatus))]
    public class HasInventaryStatus : Decorator {
        private BotBrainData _brainData;

        [Inject]
        public void Construct(Bot.BotModel bot) {
            _brainData = bot.BrainData;
        }

        public override NodeResult Execute() {
            if (_brainData.InventoryStatus.CurrentValue == true)
                return NodeResult.failure;

            return children[0].Execute();
        }
    }
}



