using R3;
using System;

namespace BehaviorTree.Bot {

    public interface IBotStateViewModel : IDisposable {
        public ReadOnlyReactiveProperty<BotStateViewData> CurrentBotStateViewData { get; }
    }
}



