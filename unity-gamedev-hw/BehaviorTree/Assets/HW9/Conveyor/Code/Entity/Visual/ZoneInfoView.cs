using TMPro;
using UnityEngine;

namespace Conveyors.Entity.Visual {

    public sealed class ZoneInfoView : MonoBehaviour {
        [SerializeField] private TMP_Text _infoText;

        public void UpdateInfo(string newInfo) {
            _infoText.text = newInfo;
        }
    }
}