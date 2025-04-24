using Pattern_Memento;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Unit_Spawn_System {

    public class UnitManagerView : MonoBehaviour, IManagerView {
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _damageButton;

        public Observable<Unit> OnAddButtonClicked => _addButton.OnClickAsObservable();
        public Observable<Unit> OnRemoveButtonClicked => _removeButton.OnClickAsObservable();
        public Observable<Unit> OnDamageButtonClicked => _damageButton.OnClickAsObservable();
    }
}
