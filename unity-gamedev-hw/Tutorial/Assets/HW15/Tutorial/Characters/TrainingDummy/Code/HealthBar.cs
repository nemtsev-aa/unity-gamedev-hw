using System;
using UnityEngine;
using UnityEngine.UI;

namespace Characters {

    [Serializable]
    public sealed class HealthBar : MonoBehaviour {
        [SerializeField] public Image _filler;

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        public void UpdateFiller(float value) {
            _filler.fillAmount = Mathf.Clamp01(value);
        }
    }
}