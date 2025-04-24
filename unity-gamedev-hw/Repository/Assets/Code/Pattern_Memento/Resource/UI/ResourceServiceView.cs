using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Pattern_Memento {

    public sealed class ResourceServiceView : MonoBehaviour, IManagerView {
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _removeButton;

        public Observable<Unit> OnAddButtonClicked => _addButton.OnClickAsObservable();
        public Observable<Unit> OnRemoveButtonClicked => _removeButton.OnClickAsObservable();
    }
}