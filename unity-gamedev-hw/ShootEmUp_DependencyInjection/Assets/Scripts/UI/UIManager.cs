using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IDisposable {
    public event Action StartButtonClicked;
    public event Action PauseButtonClicked;
    public event Action FinishButtonClicked;

    [SerializeField] private CountdownPanel _countdownPanel;
    [SerializeField] private ÑontrolsPanel _ñontrolsPanel;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _finishButton;

    public void Init() {
        AddListenets();
        ShowDefaultState();
    }

    public void ShowDefaultState() {
        _countdownPanel.Show(false);
        _ñontrolsPanel.Show(false);

        _startButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _finishButton.gameObject.SetActive(false);
    }

    private void AddListenets() {
        _countdownPanel.CountdownFinished += OnCountdownPanel_CountdownFinished;

        _startButton.onClick.AddListener(StartButtonClick);
        _pauseButton.onClick.AddListener(PauseButtonClick);
        _finishButton.onClick.AddListener(FinishButtonClick);
    }

    private void RemoveListeners() {
        _countdownPanel.CountdownFinished -= OnCountdownPanel_CountdownFinished;

        _startButton.onClick.RemoveListener(StartButtonClick);
        _pauseButton.onClick.RemoveListener(PauseButtonClick);
        _finishButton.onClick.RemoveListener(FinishButtonClick);
    }

    private void OnCountdownPanel_CountdownFinished() {
        ShowGameplayState();
        StartButtonClicked?.Invoke();
    }

    private void StartButtonClick() {
        _countdownPanel.Show(true);
        _startButton.gameObject.SetActive(false);
    }

    private void PauseButtonClick() {
        PauseButtonClicked?.Invoke();
    }

    private void FinishButtonClick() {
        FinishButtonClicked?.Invoke();
        ShowDefaultState();
    }

    private void ShowGameplayState() {
        _countdownPanel.Show(false);
        _ñontrolsPanel.Show(true);

        _startButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        _finishButton.gameObject.SetActive(true);
    }

    public void Dispose() {
        RemoveListeners();
        ShowDefaultState();
    }
}
