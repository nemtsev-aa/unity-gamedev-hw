using UnityEngine;

namespace CursorChangeService {

    public class InteractableObject : MonoBehaviour, ICursorChanger {
        [SerializeField] protected CursorType CursorType = CursorType.Interact;

        public virtual CursorType GetCursorType() {
            return CursorType;
        }
    }
}

