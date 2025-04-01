using ShootEmUp;
using Zenject;

public sealed class GameplayInstaller : MonoInstaller {

    public override void InstallBindings() {
        BindGameCycleInstaller();
        BindGameMediator();
    }

    private void BindGameCycleInstaller() {
        Container.Bind<GameCycleInstaller>().AsSingle();
    }

    private void BindGameMediator() {
        Container.Bind<GameMediator>().AsSingle();
    }
}
