using System;

namespace UpgradesSystem.UI {

    [Serializable]
    public sealed class UICompanents {
        public UICompanents(UpgradesPopupView popup,
                            UpgradesPopupViewModelFactory popupViewModelFactory) {
            Popup = popup;
            ViewModelFactory = popupViewModelFactory;
        }

        public UpgradesPopupView Popup { get; private set; }
        public UpgradesPopupViewModelFactory ViewModelFactory { get; private set; }
    }
}
