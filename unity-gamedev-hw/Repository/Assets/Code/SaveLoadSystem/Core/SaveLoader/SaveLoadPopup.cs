using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Pattern_Memento {
    public class SaveLoadPopup : MonoBehaviour {
        [Space(10)]
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;

        public Observable<Unit> OnSaveButtonClicked => _saveButton.OnClickAsObservable();
        public Observable<Unit> OnLoalButtonClicked => _loadButton.OnClickAsObservable();
    }
}
