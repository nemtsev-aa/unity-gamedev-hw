using System;
using Zenject;
using UnityEngine;
using CharacterInfoWidget;
using System.Collections.Generic;

namespace EquipmentSystem.UI {

    public sealed class CharacterEquipmentPopup : MonoBehaviour {
        [SerializeField] private EquipmentSlotsPresenter _slotPresenter;
        [SerializeField] private EquipmentPresenter _equipmentPresenter;
        [SerializeField] private CharacterInfoView _characterInfoView;

        [Inject]
        public void Construct(CharacterInfoViewModel infoViewModel) {
            _characterInfoView.Init(infoViewModel);
        }

        public void Show(bool status) {
            gameObject.SetActive(status);

            _slotPresenter.Show(status);
            _equipmentPresenter.Show(status);
        }
    }
}
