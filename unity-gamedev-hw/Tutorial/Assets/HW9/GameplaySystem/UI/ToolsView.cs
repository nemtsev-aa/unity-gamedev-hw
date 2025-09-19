using R3;
using UnityEngine;
using UnityEngine.UI;

namespace GameplaySystem {

    public sealed class ToolsView : MonoBehaviour {
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _upgradesButton;
        [SerializeField] private Button _sellButton;

        public Observable<Unit> ShopButtonClicked => _shopButton.OnClickAsObservable();
        public Observable<Unit> UpgradesButtonClicked => _upgradesButton.OnClickAsObservable();
        public Observable<Unit> SellButtonClicked => _sellButton.OnClickAsObservable();
    }
}

