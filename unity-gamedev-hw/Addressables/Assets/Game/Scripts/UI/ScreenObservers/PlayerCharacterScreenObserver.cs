using R3;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UI.Components.Screens;
using UI.Core;
using CharactersSystem.Spawner;
using CharactersSystem.Player.Skins;

namespace UI.Services.Observers {

    public class PlayerCharacterScreenObserver : IDisposable {
        private readonly UIManager _uiManager;
        private readonly CharacterSpawner _characterSpawner;
        private readonly CompositeDisposable _disposables = new();

        private PlayerCharacterSkinScreen _skinScreen;
        private int _currentSkinId;

        private PlayerCharacterSkinManager CurrentSkinManager {
            get {

                var currentPlayerCharacter = _characterSpawner.CurrentPlayerCharacter;

                if (currentPlayerCharacter == null)
                    return null;

                return currentPlayerCharacter.SkinManager;
            }
        }

        public PlayerCharacterScreenObserver(UIManager uiManager,
                                            CharacterSpawner characterSpawner) {

            _uiManager = uiManager;
            _characterSpawner = characterSpawner;

            CreateViewModel();
        }

        private void CreateViewModel() {
            _skinScreen = _uiManager.GetActiveScreen<PlayerCharacterSkinScreen>(UIScreenType.PlayerCharacterSkinScreen);

            if (_skinScreen == null) {
                CreateSubscribes();
                return;
            }

            if (CurrentSkinManager == null)
                return;

            var viewModel = new PlayerCharacterSkinScreenViewModel(CurrentSkinManager);
            _skinScreen.SetViewModel(viewModel);

            _skinScreen.SkinSelected
                .Subscribe(OnSkinSelected)
                .AddTo(_disposables);
        }

        private void CreateSubscribes() {

            _uiManager.ScreenOpened
                .Where(screenType => screenType == UIScreenType.PlayerCharacterSkinScreen)
                .Subscribe(OnPlayerCharacterSkinScreenOpened)
                .AddTo(_disposables);
        }

        private void OnPlayerCharacterSkinScreenOpened(UIScreenType type) {
            CreateViewModel();
        }

        private void OnSkinSelected(int id) {

            if (CurrentSkinManager != null) {
                CurrentSkinManager.SwitchSkin(id);
                _currentSkinId = id;
            }

            _uiManager.HideScreenAsync(UIScreenType.PlayerCharacterSkinScreen).Forget();
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();

            _currentSkinId = 0;
            _skinScreen?.Dispose();
            _skinScreen = null;
        }
    }
}