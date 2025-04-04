using ShootEmUp;
using Zenject;

public sealed class GameplayInstaller : MonoInstaller {

    public override void InstallBindings() {
        BindGameCycle();
        BindGameMediator();
        BindGameCycleInstaller();
    }

    private void BindGameCycle() {
        Container.BindInterfacesAndSelfTo<GameCycle>().AsSingle();
    }

    private void BindGameMediator() {
        Container.Bind<GameMediator>().AsSingle();
    }

    private void BindGameCycleInstaller() {
        Container.Bind<GameCycleInstaller>().AsSingle().NonLazy();
    }
}
