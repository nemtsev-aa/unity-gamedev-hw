using Zenject;
using UnityEngine;
using FarmingSystem;
using UIManager = BehaviorTree.Gameplay.UIManager;

namespace BehaviorTree.Brain {

    public sealed class GameplayInstaller : MonoInstaller {
        [SerializeField] private ResourceLootSystemInstaller _lootSpawnerInstaller;
        [SerializeField] private FellingZone _fellingZone;
        [SerializeField] private UIManager _uiManager;

        public override void InstallBindings() {

            Container.BindInstance(_fellingZone)
                .AsSingle()
                .NonLazy();

            _lootSpawnerInstaller.Install(Container);
            _uiManager.Install(_fellingZone);
        }
    }
}



