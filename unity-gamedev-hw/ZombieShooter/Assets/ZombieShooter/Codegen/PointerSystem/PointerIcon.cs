using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace AtomicFramework.EnemyPointerSystem {

    public class PointerIcon : MonoBehaviour {

        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
        [Space(10)]
        [SerializeField] private float _animationDuration = 1f;

        private bool _isShown = true;

        public void Init() {
            _image.enabled = false;
            _isShown = false;
        }

        public void SetIconPosition(Vector3 position, Quaternion rotation) {
            transform.position = position;
            transform.rotation = rotation;
        }

        public void SetDistanceToEnemy(float magnitude) {
            _text.text = $"{(int)magnitude}";
        }

        public void Show() {

            if (_isShown == true)
                return;

            _isShown = true;
            _image.enabled = true;

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _animationDuration)
                     .SetEase(Ease.InElastic);
        }

        public void Hide() {

            if (_isShown == false)
                return;

            _isShown = false;
            transform.DOScale(Vector3.zero, _animationDuration / 2f)
                     .SetEase(Ease.InBack);

            _image.enabled = false;
        }
    }
}