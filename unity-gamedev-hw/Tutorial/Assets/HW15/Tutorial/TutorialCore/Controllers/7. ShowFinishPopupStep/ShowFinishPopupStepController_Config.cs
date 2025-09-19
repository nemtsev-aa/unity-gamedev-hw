using System;
using UnityEngine;
using Tutorial.UI;

namespace Tutorial.Core {

    [Serializable]
    public sealed class ShowFinishPopupStepController_Config {
        [field: SerializeField] public TutorialFinishPopup Popup { get; private set; }
    }
}