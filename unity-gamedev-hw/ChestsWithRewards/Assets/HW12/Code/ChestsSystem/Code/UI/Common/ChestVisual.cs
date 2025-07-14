using UnityEngine;
using UnityEngine.UI;

namespace ChestsSystem {


    public sealed class ChestVisual : MonoBehaviour {
        [field: SerializeField] public ChestType Type { get; private set; }
        [field: SerializeField] public Image TopPart { get; private set; }
        [field: SerializeField] public Image DownPart { get; private set; }
    }
}
