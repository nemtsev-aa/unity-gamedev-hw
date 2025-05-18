using Atomic.Contexts;
using UnityEngine;

namespace AtomicFramework.Contextes {

    public sealed class GameContext : SingletonSceneContext<GameContext> {
        [SerializeField] private GameContextInstaller _gameContextInstaller;

        protected override void Awake() {
            base.Awake();

            _gameContextInstaller.Install(this);
        }
    }
}

