using R3;
using TMPro;
using System;
using UnityEngine;

namespace FarmingSystem {

    public sealed class FellingZoneView : MonoBehaviour, IDisposable {
        [SerializeField] private TMP_Text _treeAmountText;
        [SerializeField] private TMP_Text _lootAmountText;

        private readonly CompositeDisposable _disposables = new();
        private IFellingZoneViewModel _viewModel;

        public void Init(IFellingZoneViewModel viewModel) {
            _viewModel = viewModel;

            _viewModel.ReactiveTreesAmount
                .Subscribe(TreesAmountChanged)
                .AddTo(_disposables);

            _viewModel.ReactiveLootAmount
                .Subscribe(LootAmountChanged)
                .AddTo(_disposables);
        }

        private void TreesAmountChanged(int value) {
            _treeAmountText.text = $"{value}";
        }

        private void LootAmountChanged(int value) {
            _lootAmountText.text = $"{value}";
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}
