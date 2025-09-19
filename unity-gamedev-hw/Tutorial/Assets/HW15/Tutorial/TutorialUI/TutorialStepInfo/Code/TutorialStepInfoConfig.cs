using System;
using UnityEngine;
using System.Collections.Generic;
using Tutorial.Core;

namespace Tutorial.UI {

    [Serializable]
    public sealed class TutorialStepInfoConfig {
        [field: SerializeField] public List<TutorialStepInfo> Configs { get; private set; }

        public bool TryGetInfoByType(TutorialStep type, out TutorialStepInfo info) {

            for (var i = 0; i < Configs.Count; i++) { 
                var iInfo = Configs[i];

                if (iInfo.Type == type) { 
                    info = iInfo;
                    return true;
                }
            }

            info = null;    
            return false;
        }
    }
}

