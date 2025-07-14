using Currencies;
using GameCycleSystem;
using UnityEngine;
using Zenject;

public sealed class GameplayInstaller : MonoInstaller {
    [SerializeField] private CurrencyInstaller _currencyInstaller;
    [SerializeField] private GameCycleInstaller _gameCycleInstaller;

    public override void InstallBindings() {
        _currencyInstaller.Install(Container);
        _gameCycleInstaller.Install(Container);

        Container.Bind<UIManager>()
         .AsSingle()
         .NonLazy();
    }
}

