using UnityEngine;

namespace NavigatorService {
    public sealed class TargetPointer : MonoBehaviour {
        public void Activate(bool status) {
            gameObject.SetActive(status);
        }
    }
}

