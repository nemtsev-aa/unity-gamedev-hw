using System;
using UnityEngine;
using Currencies.UI;

namespace Tutorial.Core {

    [Serializable]
    public sealed class TutorialMainController_Config {
        [field: SerializeField] public CurrencyProvider CurrencyProvider { get; private set; }
    }
}