using Client.Components.Teams;
using GameCycleSystem;
using R3;

namespace UICompanents {
    public sealed class WinnerViewModel : IWinnerViewModel {
        public Observable<TeamTypes> WinnerTeam { get; private set; }

        public WinnerViewModel(GameMediator mediator) {
            WinnerTeam = mediator.WinnerTeam;
        }
    }
}