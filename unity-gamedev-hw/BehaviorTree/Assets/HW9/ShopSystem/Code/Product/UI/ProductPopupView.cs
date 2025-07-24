using System;
using TMPro;
using R3;
using UnityEngine;
using UnityEngine.UI;
using ShopSystem.UI;

namespace ShopSystem.Product.UI {

    public sealed class ProductPopupView : MonoBehaviour {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _icon;

        [SerializeField] private BuyButton _buyButton;
        [SerializeField] private Button _closeButton;

        private ISaleProductViewModel _productViewModel;
        private readonly CompositeDisposable _disposable = new();

        public void Show(IViewModel viewModel) {
            
            if (viewModel is not ISaleProductViewModel productPresenter) 
                throw new Exception("Expected ProductInfo");

            _productViewModel = productPresenter;

            _title.text = _productViewModel.Title;
            _description.text = _productViewModel.Description;
            _icon.sprite = _productViewModel.Icon;
            _buyButton.SetPrice(_productViewModel.Price);

            _buyButton.Button.OnClickAsObservable()
                .Subscribe(_ => _productViewModel.BuyCommand.Execute(Unit.Default))
                .AddTo(_disposable);

            _productViewModel.CanBuy
                .Subscribe(OnMoneyChanged)
                .AddTo(_disposable);

            _closeButton.onClick.AddListener(Hide);

            gameObject.SetActive(true);
        }

        private void OnMoneyChanged(bool canBuy) {
            ButtonState buttonState = canBuy ? 
                ButtonState.Available :
                ButtonState.Locked;

            _buyButton.SetState(buttonState);
        }

        private void Hide() {
            gameObject.SetActive(false);
            _buyButton.RemoveListener(OnBuyButtonClicked);
            _closeButton.onClick.RemoveListener(Hide);
            _disposable.Clear();
        }

        private void OnBuyButtonClicked() {

            if (_disposable.IsDisposed == false)
                _disposable.Dispose();
        }
    }
}
