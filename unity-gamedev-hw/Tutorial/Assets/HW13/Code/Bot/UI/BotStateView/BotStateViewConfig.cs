using System;
using UnityEngine;
using BehaviorTree.Brain;

namespace BehaviorTree.Bot.UI {

    [Serializable]
    public sealed class BotStateViewConfig {
        [field: SerializeField] public BotStates State { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}



