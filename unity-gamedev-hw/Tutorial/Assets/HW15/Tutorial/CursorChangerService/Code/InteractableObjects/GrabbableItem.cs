using UnityEngine;

namespace CursorChangeService {
    public class GrabbableItem : InteractableObject {
        [SerializeField] private string _itemName;

        private void Start() {
            CursorType = CursorType.Grab;
        }
    }
}

