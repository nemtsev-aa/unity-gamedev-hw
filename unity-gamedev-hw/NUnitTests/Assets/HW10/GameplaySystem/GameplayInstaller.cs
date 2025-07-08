using UnityEngine;
using Zenject;

namespace GameplaySystem {

    public sealed class GameplayInstaller : MonoInstaller {
        [SerializeField] private ToolsView _toolsView;

        public override void InstallBindings() {

            Container.BindInstance(_toolsView)
                     .AsSingle();

            Container.Bind<UIManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}