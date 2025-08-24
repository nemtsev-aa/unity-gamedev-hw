using UnityEngine;

namespace UI.Components.ActiveEffectViewSystem {

    public abstract class ActiveEffectView : MonoBehaviour {
        public ActiveEffectType Type => _config.Type;

        private ActiveEffectViewConfig _config;

        protected RectTransform RectTransform;
        protected Transform OriginalParent;
        protected Vector2 OriginalPosition;

        public virtual void Init(ActiveEffectViewConfig config) {
            _config = config;

            RectTransform = GetComponent<RectTransform>();
            OriginalParent = RectTransform.parent;
            OriginalPosition = RectTransform.anchoredPosition;

            Activate(false, OriginalParent);
        }

        public virtual void Activate(bool status, Transform parent) {
            Transform currentParent = status == true ? parent : OriginalParent;
            transform.SetParent(currentParent);
            transform.localPosition = Vector3.zero;

            gameObject.SetActive(status);
        }

        public virtual void Reset() {
            Activate(false, OriginalParent);
        }
    }
}