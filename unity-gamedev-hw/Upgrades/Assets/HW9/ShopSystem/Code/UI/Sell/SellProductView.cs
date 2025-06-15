using R3;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using ShopSystem.Product.UI;

namespace ShopSystem.UI {
    public sealed class SellProductView : MonoBehaviour, IDisposable {
        [SerializeField] private SellButton _button;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Image _iconImage;

        private readonly CompositeDisposable _disposable = new();

        public ISellProductViewModel ViewModel;

        public void Initialized(ISellProductViewModel productViewModel) {
            ViewModel = productViewModel;

            _titleText.text = ViewModel.Title;
            _descriptionText.text = ViewModel.Description;
            _iconImage.sprite = ViewModel.Icon;
            _button.SetPrice(ViewModel.Price);

            _button.Button.OnClickAsObservable()
                .Subscribe(_ => ViewModel.SellCommand.Execute(Unit.Default))
                .AddTo(_disposable);

            ViewModel.CanSell
                .Subscribe(OnCanSell)
                .AddTo(_disposable);
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        private void OnCanSell(bool canSell) {
            ButtonState buttonState = canSell ?
                ButtonState.Available :
                ButtonState.Locked;

            _button.SetState(buttonState);
        }

        public void Dispose() {
            ViewModel?.Dispose();
            _disposable.Clear();
        }
    }
}
