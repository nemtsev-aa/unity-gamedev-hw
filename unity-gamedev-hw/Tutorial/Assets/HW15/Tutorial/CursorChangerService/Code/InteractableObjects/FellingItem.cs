using UnityEngine;

namespace CursorChangeService {

    public class FellingItem : InteractableObject {
        [SerializeField] private string _itemName;

        private void Start() {
            CursorType = CursorType.Felling;
        }
    }
}

