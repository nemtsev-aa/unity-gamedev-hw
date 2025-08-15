using CharactersSystem.Player;
using Cysharp.Threading.Tasks;
using GameCycleSystem;
using SampleGame;
using System;
using System.Threading;
using UnityEngine;

namespace CharactersSystem.Spawner {

    public sealed class CharacterSpawner : IGameFinishListener {
        public PlayerCharacter CurrentPlayerCharacter => _currentPlayerCharacter;

        public const int PLAYER_CHARACTER_PREFAB_INDEX = 0;
        public const int NPC_PREFAB_INDEX = 1;

        private readonly GameCycle _gameCycle;
        private readonly PlayerCharacterConfig _characterConfig;
        private readonly CharacterFactory _factory;
        private readonly MoveController _moveController;
        private readonly CameraFollower _cameraFollower;
        private readonly Transform _worldParent;
        private readonly CancellationTokenSource _cts = new();

        private PlayerCharacter _currentPlayerCharacter;

        public CharacterSpawner(GameCycle gameCycle,
                                PlayerCharacterConfig characterConfig,
                                CharacterFactory factory,
                                MoveController moveController,
                                CameraFollower cameraFollower,
                                Transform worldParent) {

            _gameCycle = gameCycle;
            _characterConfig = characterConfig;
            _factory = factory;
            _moveController = moveController;
            _cameraFollower = cameraFollower;
            _worldParent = worldParent;
        }

        public async UniTask<bool> SpawnPlayerCharacterWithCancellation(CancellationToken externalToken = default) {

            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                _cts.Token,
                externalToken
            );

            try {
                await StartSpawn(PLAYER_CHARACTER_PREFAB_INDEX, linkedCts.Token);
                return true;
            }
            catch (OperationCanceledException) {
                Debug.Log("PlayerCharacter creation has been canceled!");
                return false;
            }
            catch (Exception e) {
                Debug.LogError($"PlayerCharacter spawn error: {e.Message}");
                return false;
            }
        }

        public async UniTask<bool> SpawnNPCWithCancellation(CancellationToken externalToken = default) {

            try {
                await StartSpawn(NPC_PREFAB_INDEX, externalToken);
                return true;
            }
            catch (OperationCanceledException) {
                Debug.Log("NPC creation has been canceled!");
                return false;
            }
            catch (Exception e) {
                Debug.LogError($"NPC spawn error: {e.Message}");
                return false;
            }
        }

        private async UniTask<bool> StartSpawn(int prefabIndex, CancellationToken externalToken = default) {

            try {
                var result = await _factory.Get(prefabIndex, _worldParent, externalToken);

                if (result.TryGetComponent(out Character character) == true) {
                    var player = (PlayerCharacter)character;
                    _currentPlayerCharacter = player;

                    player.Init(_characterConfig);
                    _moveController.SetCharacter(player);
                    _cameraFollower.SetTarget(player);
                    _gameCycle.Add(player);

                    //Debug.Log($"PlayerCharacter {player.gameObject.name} has been successfully created!");
                    return true;
                }
            }
            catch (OperationCanceledException) {
                Debug.Log("PlayerCharacter creation has been canceled!");
                return false;
            }
            catch (Exception e) {
                Debug.LogError($"PlayerCharacter spawn error: {e.Message}");
                return false;
            }

            return false;
        }

        public void OnFinishGame() {
            _cts?.Dispose();
            _currentPlayerCharacter = null;
        }
    }
}