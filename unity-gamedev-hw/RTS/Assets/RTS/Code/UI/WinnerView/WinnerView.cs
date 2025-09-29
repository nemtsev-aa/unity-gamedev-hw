using R3;
using TMPro;
using System;
using UnityEngine;
using Client.Components.Teams;

namespace UICompanents {

    public sealed class WinnerView : MonoBehaviour, IDisposable {
        private const string LEBEL_INFO = "Winner: ";

        [SerializeField] private TMP_Text _label;

        private CompositeDisposable _disposables = new();
        private IWinnerViewModel _viewModel;

        public void Init(IWinnerViewModel viewModel) {
            _viewModel = viewModel;

            CreateReactiveSubscribes();
            Show(false);
        }

        private void CreateReactiveSubscribes() {

            _viewModel.WinnerTeam
                .Subscribe(WinnerTeamChanged)
                .AddTo(_disposables);
        }

        private void WinnerTeamChanged(TeamTypes team) {
            Show(true);
            _label.text = $"{LEBEL_INFO}{team}";
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}