using System;

namespace UpgradesSystem.UI {

    [Serializable]
    public sealed class UICompanents {
        public UICompanents(IUpgradesPopupView popup,
                            UpgradesPopupViewModelFactory popupViewModelFactory) {
            Popup = popup;
            ViewModelFactory = popupViewModelFactory;
        }

        public IUpgradesPopupView Popup { get; private set; }
        public UpgradesPopupViewModelFactory ViewModelFactory { get; private set; }
    }
}
