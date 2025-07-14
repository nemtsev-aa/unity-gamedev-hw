using System;
using UnityEngine;

namespace SessionTrackerSystem {

    [Serializable]
    public sealed class UICompanentsPresenter {
        [field: SerializeField] public SessionHistoryPopup HistoryPopup { get; private set; }
        [field: SerializeField] public CurrentSessionInfoPopup CurrentSessionInfoPopup { get; private set; }
        [field: SerializeField] public PopupSelectorView PopupSelectorView { get; private set; }
    }
}


