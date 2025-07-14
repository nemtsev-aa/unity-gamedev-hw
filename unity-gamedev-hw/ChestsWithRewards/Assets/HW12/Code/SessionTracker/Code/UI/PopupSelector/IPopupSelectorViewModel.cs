using GameCycleSystem;
using R3;
using System;

namespace SessionTrackerSystem {

    public interface IPopupSelectorViewModel : IViewModel, IDisposable {
        public ReadOnlyReactiveProperty<GameStates> CurrentGameState { get; }
    }
}


