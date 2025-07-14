using R3;
using SessionTrackerSystem;
using System;
using System.Collections.Generic;

namespace ChestsSystem {
    public interface IChestsPopupViewModel : IViewModel, IDisposable {
        IDictionary<ChestType, ReactiveChest> Chests { get; }
        ChestVisualProvider ChestModelProvider { get; }
        ReactiveCommand<ReactiveChest> TryOpenReactiveChest { get; }
    }
}
