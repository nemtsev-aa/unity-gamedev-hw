using UnityEngine;

namespace UpgradesSystem.UI {
    public interface IUpgradesPopupView {
        GameObject GameObject { get; }
        void Init(UpgradesPopupViewModel viewModel);
        void Show(bool status);
    }
}
