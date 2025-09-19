using System;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Tutorial.UI {

    public sealed class TutorialPointerUI : MonoBehaviour {
        public event Action AnimateCompleted;

        [SerializeField] private RectTransform _pointerIcon;
        [Space, SerializeField] private RectTransform _point1;
        [SerializeField] private RectTransform _point2;
        [SerializeField] private RectTransform _point3;

        [Space, SerializeField] private float _duration = 1f;
        [SerializeField] private float _scaleTo = 0.9f;

        [Space, SerializeField] private int _clickCount = 3;
        [SerializeField] private float _scaleDuration = 0.2f;

        [Button]
        public void Init() {
            _pointerIcon.transform.position = _point1.position;
            _pointerIcon.transform.localScale = Vector3.one;
            _pointerIcon.gameObject.SetActive(false);
        }

        [Button]
        public void AnimateObject() {
            _pointerIcon.gameObject.SetActive(true);
            _pointerIcon.transform.DOKill();

            Sequence sequence = DOTween.Sequence();
            sequence.SetDelay(0.5f);

            AddMoveToSequence(sequence, _point2);
            AddClickToSequence(sequence, _clickCount);
            AddMoveToSequence(sequence, _point3);
            AddClickToSequence(sequence, 1);

            sequence.SetAutoKill(true)
                    .SetUpdate(UpdateType.Normal);

            sequence.OnComplete(() => {
                _pointerIcon.transform.position = _point1.position;
                _pointerIcon.gameObject.SetActive(false);
                AnimateCompleted?.Invoke();
            });
        }

        private void AddMoveToSequence(Sequence sequence, RectTransform point) {
            sequence.Append(_pointerIcon.transform.DOMove(point.position, _duration)
                    .SetEase(Ease.OutQuad));
        }

        private void AddClickToSequence(Sequence sequence, int count) {

            for (int i = 0; i < count; i++) {

                sequence.Append(_pointerIcon.transform.DOScale(Vector3.one * _scaleTo, _scaleDuration)
                        .SetEase(Ease.OutBack));

                sequence.Append(_pointerIcon.transform.DOScale(Vector3.one, _scaleDuration)
                        .SetEase(Ease.OutBack));
            }
        }

        public void RestartAnimation() {
            _pointerIcon.transform.position = _point1.position;
            _pointerIcon.transform.localScale = Vector3.one;
            AnimateObject();
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_point1.position, 0.1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_point2.position, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_point3.position, 0.1f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(_point1.position, _point2.position);
            Gizmos.DrawLine(_point2.position, _point3.position);
        }
    }
}
