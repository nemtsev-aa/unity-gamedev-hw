using UnityEngine;

namespace NavigatorService {
    public sealed class NavigationPointer : MonoBehaviour {
 
        public void Activate(bool status) {
            gameObject.SetActive(status);
        }
    }
}

