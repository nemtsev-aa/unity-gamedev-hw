using System;
using UnityEngine;
using Currencies.UI;


namespace Tutorial.Core {

    [Serializable]
    public sealed class SellResourceStepController_Config {
        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField] public CurrencyProvider CurrencyProvider { get; private set; }
    }
}