using Client.Components.Common;
using Client.Components.Teams;
using R3;
using System;
using Units.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UICompanents {

    public sealed class TeamPanel : MonoBehaviour, IDisposable {
        public Observable<UnitTypes> CreationAction => _creationAction;

        [SerializeField] private Image _background;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private UnitsCreationView _unitsCreationView;

        private Subject<UnitTypes> _creationAction = new Subject<UnitTypes>();
        private CompositeDisposable _disposables = new();
        private UnitViewConfigs _unitViewConfigs;
        private TeamTypes _team;

        [Inject]
        public void Construct(UnitViewConfigs unitViewConfigs) {
            _unitViewConfigs = unitViewConfigs;
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        public void Init(TeamTypes team) {
            _team = team;
            var configList = _unitViewConfigs.GetConfigListByTeamType(team);

            _unitsCreationView.Init(new UnitsCreationViewModel(configList));
            
            _unitsCreationView.CreationClicked
                .Subscribe(OnCreationClicked)
                .AddTo(_disposables);
        }

        private void OnCreationClicked(UnitTypes type) =>
            _creationAction.OnNext(type);

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}