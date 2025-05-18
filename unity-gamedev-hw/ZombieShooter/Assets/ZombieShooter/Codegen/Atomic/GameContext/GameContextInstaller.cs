using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.CameraFollowSystem;
using AtomicFramework.EnemyPointerSystem;
using AtomicFramework.EnemySystem;
using AtomicFramework.InputSystem;
using System;
using UnityEngine;
using ZombieShooter.GameCycleSystem;
using ZombieShooter.SceneObjects;
using ZombieShooter.UI;

namespace AtomicFramework.Contextes {

    [Serializable]
    public sealed class GameContextInstaller : SceneContextInstallerBase {
        [SerializeField] private UIController _uiController;
        [Space(10)]
        [SerializeField] private ContainersPresenter _containersPresenter;
        [SerializeField] private CharacterSpawner _characterSpawner;
        [Space(10)]
        [SerializeField] private CameraFollowConfig _cameraFollowConfig;
        [SerializeField] private InputConfig _inputConfig;
        [Space(10)]
        [SerializeField] private EnemySystemInstaller _enemySystemInstaller;
        [SerializeField] private PointerSystemInstaller _pointerSystemInstaller;
        [SerializeField] private EffectSystemInstaller _effectSystemInstaller;
        [Space(10)]
        [SerializeField] private bool _showLogAfterExecution;

        public override void Install(IContext context) {
            context.AddContainersPresenter(_containersPresenter);
            context.AddCameraConfig(_cameraFollowConfig);
            context.AddInputConfig(_inputConfig);

            context.AddSystem(_characterSpawner);
            context.AddSystem(_uiController);
            context.AddSystem<GameCycle>();
            context.AddSystem<CameraFollow>();
            context.AddSystem<InputController>();
            context.AddSystem<CharacterInputHandler>();

            context.Install(_pointerSystemInstaller);
            context.Install(_enemySystemInstaller);
            context.Install(_effectSystemInstaller);

            var gameStateChangeAction = new BaseEvent<GameStates>();
            context.AddGameStateChangeAction(gameStateChangeAction);
            gameStateChangeAction.Invoke(GameStates.InitializingComponents);

            if (_showLogAfterExecution == true)
                Debug.LogWarning($"{nameof(GameContextInstaller)} installed");
        }
    }
}

