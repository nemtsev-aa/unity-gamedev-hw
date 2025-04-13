using UnityEngine;

namespace PresentationModel {
    public sealed class EntryPoint : MonoBehaviour {
        [SerializeField] private CharacterPopup _popup;
        [SerializeField] private CharacterPopupPresenterView _presenterView;
        [Space(10)]
        [SerializeField] private CharacterPopupViewModelManager _viewModelManager;
        [SerializeField] private RealTimeChangeManager _realTimeChangeManager;

        private void Start() {
            _realTimeChangeManager.enabled = false;

            _viewModelManager.Init(_presenterView);
            var popupPresenter = new CharacterPopupPresenter(_popup, _presenterView, _viewModelManager, _realTimeChangeManager);

            _presenterView.Init();
            _presenterView.Show(true);
        }
    }
}


