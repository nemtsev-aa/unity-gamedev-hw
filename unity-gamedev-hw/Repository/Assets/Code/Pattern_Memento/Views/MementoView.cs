using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pattern_Memento {

    public partial class MementoView : MonoBehaviour, IDisposable {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _checkMark;
        [SerializeField] private Button _selectionButton;
        
        private readonly Subject<MementoView> _viewSelected = new();

        public string ID { get; private set; }
        public bool IsSelected { get; private set; }
        public Observable<MementoView> ViewSelected => _viewSelected;

        public void Init(IMementoViewModel memento) {
            ID = memento.ID;

            UpdateCompanents();
            AddListeners();
        }

        public void SetSelectionStatus(bool value) {
            IsSelected = value;
            _checkMark.gameObject.SetActive(IsSelected);
        }

        public void Reset() {
            ID = "";
            _name.text = "";
        }

        private void UpdateCompanents() {
            _name.text = ID;
        }

        private void AddListeners() {
            _selectionButton.onClick.AddListener(OnSelectionButtonClick);
        }

        private void RemoveListeners() {
            _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
        }

        private void OnSelectionButtonClick() {
            IsSelected = !IsSelected;

            _checkMark.gameObject.SetActive(IsSelected);
            _viewSelected.OnNext(this);
        }

        public void Dispose() {
            RemoveListeners();
        }
    }
}
