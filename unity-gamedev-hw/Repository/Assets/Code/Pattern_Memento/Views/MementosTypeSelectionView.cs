using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Pattern_Memento {

    public class MementosTypeSelectionView : MonoBehaviour {
        [SerializeField] private Button _unitPopupShowButton;
        [SerializeField] private Button _resourcePopupShowButton;

        public Observable<Unit> OnUnitPopupShowButtonClicked => _unitPopupShowButton.OnClickAsObservable();
        public Observable<Unit> OnResourcePopupShowButton => _resourcePopupShowButton.OnClickAsObservable();
    }
}
