using UnityEngine;
using UnityEngine.UI;

namespace InteractionService {

    public sealed class InteractionHandlerView : MonoBehaviour {
        [SerializeField] private InteractionIconConfigs _config;
        [SerializeField] private Image _filledImage;
        [SerializeField] private Image _iconImage;

        public void Init() => Show(false);

        public void Show(bool status, InteractionTypes type = InteractionTypes.None) {

            if (status == true && type != InteractionTypes.None)
                _iconImage.sprite = _config.GetSpriteByInteractionType(type);

            gameObject.SetActive(status);
        }

        public void UpdateCompanent(float value) {
            _filledImage.fillAmount = value;
        }
    }
}
