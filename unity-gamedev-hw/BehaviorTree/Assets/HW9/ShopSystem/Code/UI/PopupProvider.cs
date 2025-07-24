using System;
using UnityEngine;

namespace ShopSystem.UI {

    [Serializable]
    public sealed class PopupProvider {
        [field: SerializeField] public ShopPopupView ShopPopup { get; private set; }
        [field: SerializeField] public SellPopupView SellPopup { get; private set; }
    }
}
