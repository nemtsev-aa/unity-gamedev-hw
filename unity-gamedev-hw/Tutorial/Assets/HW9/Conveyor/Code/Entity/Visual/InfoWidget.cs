using UnityEngine;
using UnityEngine.UI;

namespace Conveyors.Entity.Visual {

    [AddComponentMenu("Gameplay/Conveyors/Conveyor Info Widget")]
    public sealed class InfoWidget : MonoBehaviour {

        [SerializeField] private GameObject _root;
        [SerializeField] private Image _inputImage;
        [SerializeField] private Image _outputImage;
        [SerializeField] private ProgressBar _progressBar;

        public ProgressBar ProgressBar => _progressBar;

        public void SetVisible(bool isVisible) {
            _root.SetActive(isVisible);
        }

        public void SetInputIcon(Sprite icon) {
            _inputImage.sprite = icon;
        }

        public void SetOutputIcon(Sprite icon) {
            _outputImage.sprite = icon;
        }
    }
}