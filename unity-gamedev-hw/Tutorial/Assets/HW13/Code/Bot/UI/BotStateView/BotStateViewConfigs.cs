using BehaviorTree.Brain;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Bot.UI {

    [CreateAssetMenu(
        fileName = nameof(BotStateViewConfigs),
        menuName = "BehaviorTree/" + nameof(BotStateViewConfigs)
    )]
    public sealed class BotStateViewConfigs : ScriptableObject {
        [field: SerializeField] public List<BotStateViewConfig> Configs { get; private set; }

        public BotStateViewConfig GetConfigByType(BotStates state) {

            for (int i = 0; i < Configs.Count; i++) {
                var iConfig = Configs[i];

                if (iConfig.State == state)
                    return iConfig;
            }

            throw new ArgumentException($"BotStates: {state} not found!");
        }
    }
}



