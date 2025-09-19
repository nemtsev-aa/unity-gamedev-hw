using UnityEngine;
using UnityEngine.UI;

namespace Conveyors.Entity.Visual {

    public sealed class ProgressBar : MonoBehaviour {
        [SerializeField] private GameObject _root;
        [SerializeField] private Image _fillImage;
        [Space, SerializeField] private bool _hasMask;
        [SerializeField] private Image _maskImage;

        public void SetVisible(bool isVisible) {
            _root.SetActive(isVisible);
        }

        public void SetProgress(float progress) {
            if (_hasMask) {
                _maskImage.fillAmount = progress;
            } else {
                _fillImage.fillAmount = progress;
            }
        }

        public void SetColor(Color color) {
            _fillImage.color = color;
        }
    }
}