using UnityEngine;

namespace PresentationModel {
    public abstract class UIView : MonoBehaviour {
        public abstract void Init(IViewModel viewModel);
        public abstract void UpdateCompanents();
    }
}


