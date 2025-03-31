using System;

namespace ShootEmUp {
    public sealed class GameMediator : IDisposable, IGameStartListener, IGameFinishListener {
        public event Action<Unit> CharacterIsDeath;

        private GameCycle _gameCycle;
        private UIManager _uIManager;
        private Character _character;
        private InputManager _input;

        public GameMediator(GameCycle gameCycle, UIManager uIManager, Character character, InputManager input) {
            _gameCycle = gameCycle;
            _uIManager = uIManager;
            _character = character;
            _input = input;

            SubscribeToUIEvents();
        }

        public void OnStartGame() {
            _character.Death += CharacterDeath;

            _input.HorizontalDirectionChanged += OnInput_HorizontalDirectionChanged;
            _input.FireStatusChanged += OnInput_FireStatusChanged;
            _input.PauseStatusChanged += PauseGameplay;
        }

        public void OnFinishGame() {
            _character.Death -= CharacterDeath;

            _input.HorizontalDirectionChanged -= OnInput_HorizontalDirectionChanged;
            _input.FireStatusChanged -= OnInput_FireStatusChanged;
            _input.PauseStatusChanged -= PauseGameplay;
        }

        private void CharacterDeath(Unit unit) {
            CharacterIsDeath?.Invoke(unit);

            _uIManager.ShowDefaultState();
            FinishGameplay();
        }

        private void OnInput_HorizontalDirectionChanged(int value) =>
            _character.SetHorizontalDirection(value);

        private void OnInput_FireStatusChanged() =>
            _character.SetFireStatus();

        private void StartGameplay() =>
            _gameCycle.StartGame();

        private void PauseGameplay() =>
            _gameCycle.PauseGame();

        private void FinishGameplay() =>
            _gameCycle.FinishGame();

        private void SubscribeToUIEvents() {
            _uIManager.StartButtonClicked += StartGameplay;
            _uIManager.PauseButtonClicked += PauseGameplay;
            _uIManager.FinishButtonClicked += FinishGameplay;
        }

        private void UnSubscribeToUIEvents() {
            _uIManager.StartButtonClicked -= StartGameplay;
            _uIManager.PauseButtonClicked -= PauseGameplay;
            _uIManager.FinishButtonClicked -= FinishGameplay;
        }

        public void Dispose() {
            OnFinishGame();
            UnSubscribeToUIEvents();
        }
    }
}