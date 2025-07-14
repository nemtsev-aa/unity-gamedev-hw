using GameCycleSystem;
using SessionTrackerSystem;

public sealed class UIManager {
    private readonly GameCycle_UI _gameCycle_UI;
    private readonly SessionTracker_UI _sessionTracker_UI;

    public UIManager(GameCycle_UI gameCycle_UI,
                    SessionTracker_UI sessionTracker_UI) {

        _gameCycle_UI = gameCycle_UI;
        _sessionTracker_UI = sessionTracker_UI;
    }

    public void ShowGameCycleView() =>
        _gameCycle_UI.ShowGameCycleView();

    public void ShowPopupSelectorView() =>
        _sessionTracker_UI.ShowPopupSelectorView();
}

