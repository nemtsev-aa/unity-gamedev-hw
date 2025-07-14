using System;
using UnityEngine;
using Zenject;

namespace GameCycleSystem {

    [Serializable]
    public sealed class GameCycleInstaller {
        [SerializeField] private GameCycleView _gameCycleView;

        public void Install(DiContainer container) {
            container.BindInterfacesAndSelfTo<GameCycle>()
                     .AsSingle();

            container.Bind<GameCycleInitializer>()
             .AsSingle()
             .NonLazy();

            var gameCycle = container.Resolve<GameCycle>();
            var viewModel = new GameCycleViewModel(gameCycle);
            _gameCycleView.Init(viewModel);

            container.BindInstance(_gameCycleView)
                .AsSingle()
                .NonLazy();

            container.Bind<GameCycle_UI>()
                .AsSingle()
                .NonLazy();
        }
    }
}



