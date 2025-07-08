using System;
using Zenject;
using InventorySystem.Core;
using InventorySystem.EffectObservers;

namespace InventorySystem.Installers {

    [Serializable]
    public sealed class InventoryCoreInstaller {
        
        public void Bind(DiContainer container) {

            var newInventory = new Inventory();

            container.BindInterfacesAndSelfTo<InventoryService>()
                     .AsSingle()
                     .WithArguments(newInventory)
                     .NonLazy();

            container.Bind<InventoryItemConsumer>()
                     .AsSingle()
                     .NonLazy();

            container.Bind<InventoryEffectObserver>()
                     .AsSingle()
                     .WithArguments(newInventory)
                     .NonLazy();

            container.Bind<MedicalKitConsumeObserver>()
                     .AsSingle()
                     .NonLazy();

            container.Bind<InventoryHelper>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}
