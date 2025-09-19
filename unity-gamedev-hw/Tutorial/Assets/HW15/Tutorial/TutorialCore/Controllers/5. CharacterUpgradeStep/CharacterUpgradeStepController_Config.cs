using System;
using UnityEngine;
using BehaviorTree.PlayerCoreSubsystem;
using Currencies.UI;
using NavigatorService;

namespace Tutorial.Core {

    [Serializable]
    public sealed class CharacterUpgradeStepController_Config {
        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField] public CurrencyProvider CurrencyProvider { get; private set; }
        [field: SerializeField] public string UpgradeName { get; private set; }
        [field: SerializeField] public int UpgradeMaxLevel { get; private set; }
    }
}