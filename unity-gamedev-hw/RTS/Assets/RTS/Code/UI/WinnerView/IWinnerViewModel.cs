using Client.Components.Teams;
using R3;

namespace UICompanents {
    public interface IWinnerViewModel {
        Observable<TeamTypes> WinnerTeam { get; }
    }
}