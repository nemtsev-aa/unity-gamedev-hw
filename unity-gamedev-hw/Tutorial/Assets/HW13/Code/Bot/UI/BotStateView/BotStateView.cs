using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTree.Bot.UI {

    public sealed class BotStateView : MonoBehaviour, IDisposable {
        [SerializeField] private Image _stateIcon;
        [SerializeField] private TMP_Text _stateName;

        private CompositeDisposable _disposables = new();
        private IBotStateViewModel _viewModel;

        public void Init(IBotStateViewModel viewModel) {
            _viewModel = viewModel;

            CreateReactiveSubscribes();
        }

        private void CreateReactiveSubscribes() {

            _viewModel.CurrentBotStateViewData
                .Subscribe(BotStateChanged)
                .AddTo(_disposables);
        }

        private void BotStateChanged(BotStateViewData data) {

            _stateIcon.sprite = data.Icon;
            _stateName.text = $"{data.Description}";
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}



