using Zenject;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using InventorySystem.Core;
using EquipmentSystem.Core;

namespace EquipmentSystem.UI {

    public sealed class EquipmentSlotsPresenter : MonoBehaviour {
        [SerializeField] private EquipmentSlotView _prefab;
        [SerializeField] private RectTransform _container;
        [SerializeField] private List<EquipmentSlotPosition> _points;

        private List<EquipmentSlotView> _slotViews = new List<EquipmentSlotView>();
        private IReadOnlyList<EquipmentSlot> _equipmentSlots;
        private DragDropLogicController _dragDropLogicController;
        private EquipmentService _service;

        [Inject]
        public void Construct(EquipmentService service,
                              DragDropLogicController dragDropLogicController) {

            _service = service;
            _service.SlotEquiped += OnSlotEquiped;

            _equipmentSlots = _service.Slots;
            _dragDropLogicController = dragDropLogicController;
            _dragDropLogicController.DragStarted += OnDragStarted;
            _dragDropLogicController.DropComplited += OnDropComplited;
        }

        public void Show(bool status) {

            gameObject.SetActive(status);

            if (status == false)
                return;

            if (_slotViews.Count == 0 && _equipmentSlots.Count > 0)
                CreateSlotViews();

            UpdateEquipSlots();
        }

        private void CreateSlotViews() {

            foreach (var iSlot in _equipmentSlots) {

                var view = Instantiate(_prefab, _container);
                view.gameObject.name = $"EquipmentSlotView [{iSlot.Index}] ({iSlot.Type})";
                view.Init(iSlot);

                var iConfig = _points.FirstOrDefault(c => c.Index == iSlot.Index);
                var spawnPointPosition = iConfig.SpawnPoint.position;

                view.transform.position = spawnPointPosition;

                _slotViews.Add(view);
            }
        }

        private void UpdateEquipSlots() {

            for (int i = 0; i < _slotViews.Count; i++) {
                var slotView = _slotViews[i];
                var item = _service.GetEquippedItem(i);
                slotView.SetItem(item);
            }
        }

        private void OnSlotEquiped(EquipmentSlot slot, InventoryItem item) {

            if (item == null)
                return;

            var slotView = _slotViews.FirstOrDefault(s => s.Index == slot.Index);

            if (slotView.CurrentItem != slot.Item) {
                slotView.SetItem(item);
                //Debug.Log($"EquipmentSlotPresenter: UpdateSlot [{slot.Index}]");
            }
        }

        private void OnDragStarted(EquipSlotTypes currentSlotType) {

            foreach (var iSlotView in _slotViews) {

                if (iSlotView.SlotType == currentSlotType) 
                    iSlotView.StartHighlightAnimation(Color.green);
            }
        }

        private void OnDropComplited() {

            foreach (var iSlotView in _slotViews) {

                iSlotView.StopHighlightAnimation(
                    iSlotView.CurrentItem != null ?
                    Color.white : Color.clear);
            }
        }

        private void OnDestroy() {
            _service.SlotEquiped -= OnSlotEquiped;
            _dragDropLogicController.DragStarted -= OnDragStarted;
            _dragDropLogicController.DropComplited -= OnDropComplited;
        }
    }
}
