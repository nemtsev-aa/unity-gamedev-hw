using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using GameCycleSystem;
using Cysharp.Threading.Tasks;
using CharactersSystem.Spawner;
using SampleLevelZoneSystemGame;
using UI.Core;
using UI.Services.Observers;

namespace SampleGame.Core
    {

    public sealed class GameplayBootstrapper : MonoBehaviour, IGameFinishListener {
        private GameCycle _gameCycle;
        private CharacterSpawner _spawner;
        private ZoneManager _zoneManager;
        private UIManager _uiManager;

        private CancellationTokenSource _cts;
        private PlayerCharacterScreenObserver _playerCharacterScreenObserver;

        [Inject]
        public void Construct(GameCycle gameCycle,
                              CharacterSpawner spawner,
                              ZoneManager zoneManager,
                              UIManager uiManager) {

            _gameCycle = gameCycle;
            _spawner = spawner;
            _zoneManager = zoneManager;
            _uiManager = uiManager;

            _cts = new CancellationTokenSource();
        }

        private async void Start() {
            await StartGameplayAsync();
        }

        private async UniTask StartGameplayAsync() {

            if (TryFindSceneContext(out SceneContext sceneContext) == false)
                throw new ArgumentNullException($"GameplayBootstrapper: SceneContext not found!");

            await WaitForSceneContextInitialization(sceneContext);

            await _zoneManager.Init();
            await _spawner.SpawnPlayerCharacterWithCancellation(_cts.Token);

            _playerCharacterScreenObserver = new PlayerCharacterScreenObserver(_uiManager, _spawner);
            _gameCycle.Add(this);
            _gameCycle.StartGame();
        }

        private bool TryFindSceneContext(out SceneContext sceneContext) {
            var context = FindAnyObjectByType<SceneContext>();

            if (context != null) {
                sceneContext = context;
                return true;
            }

            sceneContext = null;
            return false;
        }

        private async Task WaitForSceneContextInitialization(SceneContext sceneContext) {

            while (sceneContext.HasResolved == false) {
                await Task.Yield();
            }
        }

        public void CancelSpawning() {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public void OnFinishGame() {
            _spawner = null;
            _zoneManager = null;
            _uiManager = null;
            _cts?.Dispose();
            _cts = null;

            _playerCharacterScreenObserver?.Dispose();
        }
    }
}