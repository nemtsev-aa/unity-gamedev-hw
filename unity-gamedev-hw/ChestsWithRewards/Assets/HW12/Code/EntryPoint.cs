using Zenject;
using UnityEngine;
using GameCycleSystem;
using SessionTrackerSystem;

public sealed class EntryPoint : MonoBehaviour {
    private GameCycle _gameCycle;
    private ISessionDataSaver _saver;
    private UIManager _uIManager;

    [Inject]
    public void Construct(GameCycle gameCycle,
                         ISessionDataSaver saver,
                         UIManager uIManager) {

        _gameCycle = gameCycle;
        _saver = saver;
        _uIManager = uIManager;

        JsonProjectSettings.ApplyProjectSerializationSettings();
    }

    public void Start() {
        _gameCycle.InitializeGame();
        _uIManager.ShowGameCycleView();
        _uIManager.ShowPopupSelectorView();
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.C) == true) 
            _saver.TryClearSessionData();
    }
}

