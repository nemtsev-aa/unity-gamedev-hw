using EquipmentSystem.Core;
using EquipmentSystem.Observers;
using EquipmentSystem.UI;
using InventorySystem.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace EquipmentSystem.Installers {

    public sealed class EquipmentSystemInstaller : MonoInstaller {
        [SerializeField] private EquipmentServiceConfig _config;
        [Header("DragDropMechanics")]
        [SerializeField] private Canvas _dragDropCanvas;
        [SerializeField] private Image _dragIconPrefab;
        [Header("Popups")]
        [SerializeField] private CharacterEquipmentPopup _equipmentPopup;

        public override void InstallBindings() {

            Container.BindInterfacesAndSelfTo<EquipmentService>()
                .AsSingle()
                .WithArguments(_config)
                .NonLazy();

            Container.Bind<EquipmentEffectObserver>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<DragDropVisualController>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName("DragDropVisualController")
                .AsSingle()
                .OnInstantiated<DragDropVisualController>(OnVisualControllerInstantiated)
                .NonLazy();

            Container.Bind<DragDropLogicController>()
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_equipmentPopup)
                .AsSingle()
                .NonLazy();
        }

        private void OnVisualControllerInstantiated(InjectContext context, DragDropVisualController controller) {
            controller.Initialize(_dragDropCanvas, _dragIconPrefab);
        }
    }
}
