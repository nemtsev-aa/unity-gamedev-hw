using UnityEngine;
using Zenject;

namespace SampleGame.Gameplay {

    public sealed class GameplayInstaller : MonoInstaller {
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private Camera _camera;
        [SerializeField] private InputConfig _inputConfig;

        public override void InstallBindings() {
            BindGameplayCompanents();
        }

        private void BindGameplayCompanents() {
            Container
                .Bind<Camera>()
                .FromInstance(_camera);

            Container.BindInterfacesAndSelfTo<MoveController>()
                .AsCached()
                .NonLazy();

            Container
                .Bind<IMoveInput>()
                .To<MoveInput>()
                .AsSingle()
                .WithArguments(_inputConfig)
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<CameraFollower>()
                .AsCached()
                .WithArguments(_cameraConfig.cameraOffset)
                .NonLazy();
        }
    }
}