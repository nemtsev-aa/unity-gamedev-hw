using R3;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using ShopSystem.Product.UI;

namespace ShopSystem.UI {

    public class ProductView : MonoBehaviour, IDisposable {
        [SerializeField] private BuyButton _button;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Image _iconImage;

        private ISaleProductViewModel _productViewModel;
        private readonly CompositeDisposable _disposable = new();

        public void Initialized(ISaleProductViewModel productViewModel) {
            _productViewModel = productViewModel;

            _titleText.text = _productViewModel.Title;
            _descriptionText.text = _productViewModel.Description;
            _iconImage.sprite = _productViewModel.Icon;
            _button.SetPrice(_productViewModel.Price);

            _button.Button.OnClickAsObservable()
                .Subscribe(_ => _productViewModel.BuyCommand.Execute(Unit.Default))
                .AddTo(_disposable);

            _productViewModel.CanBuy
                .Subscribe(OnCanBuy)
                .AddTo(_disposable);
        }

        private void OnCanBuy(bool canBuy) {
            ButtonState buttonState = canBuy ?
                ButtonState.Available : 
                ButtonState.Locked;

            _button.SetState(buttonState);
        }

        public void Dispose() {
            _productViewModel?.Dispose();

            if (_disposable.IsDisposed == false)
                _disposable.Dispose();
        }
    }
}
