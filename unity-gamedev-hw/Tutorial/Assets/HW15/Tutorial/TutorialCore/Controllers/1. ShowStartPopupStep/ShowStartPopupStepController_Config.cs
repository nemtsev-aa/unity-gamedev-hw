using System;
using UnityEngine;
using Tutorial.UI;

namespace Tutorial.Core {

    [Serializable]
    public sealed class ShowStartPopupStepController_Config {
        [field: SerializeField] public TutorialStartPopup Popup { get; private set; }
    }
}