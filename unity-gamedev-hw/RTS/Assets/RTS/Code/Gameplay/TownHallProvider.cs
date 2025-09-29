using Client.Installer;
using UnityEngine;

namespace GameCycleSystem {
    public sealed class TownHallProvider : MonoBehaviour {
        [field: SerializeField] public TownHallInstaller Blue { get; private set; }
        [field: SerializeField] public TownHallInstaller Red { get; private set; }
    }
}