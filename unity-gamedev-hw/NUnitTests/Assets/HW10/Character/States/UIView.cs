using UnityEngine;

namespace Character.UI {

    public abstract class UIView : MonoBehaviour {
        public abstract void Init(IViewModel viewModel);
        public abstract void UpdateCompanents();
    }
}