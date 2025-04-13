using R3;
using UnityEngine;

namespace PresentationModel {
    public sealed class PlayerLevelViewModel : IPlayerLevelViewModel {
        private readonly ReactiveProperty<bool> _canLevelUp = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public PlayerLevel PlayerLevel { get; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int RequiredExperience => PlayerLevel.RequiredExperience;

        public ReactiveCommand LevelUpCommand { get; private set; }
        public ReactiveProperty<bool> CanLevelUp => _canLevelUp;

        public PlayerLevelViewModel(PlayerLevel playerLevel) {
            PlayerLevel = playerLevel;

            UpdateCompanents();
            CreteReactiveSubscribes();
        }

        private void UpdateCompanents() {
            Level = PlayerLevel.CurrentLevel.CurrentValue;
            Experience = PlayerLevel.CurrentExperience.CurrentValue;
        }

        private void CreteReactiveSubscribes() {
            PlayerLevel.CurrentLevel
                .Subscribe(OnCurrentLevelChange)
                .AddTo(_compositeDisposable);

            PlayerLevel.CurrentExperience
                .Subscribe(OnCurrentExperienceChange)
                .AddTo(_compositeDisposable);

            LevelUpCommand = new ReactiveCommand();
            LevelUpCommand 
                .Subscribe(OnLevelUpCommand)
                .AddTo(_compositeDisposable);
        }

        private void OnCurrentLevelChange(int level) {
            Level = level;
        }

        private void OnCurrentExperienceChange(int experience) {
            Experience = experience;     
        }

        public void LevelUp() => PlayerLevel.LevelUp();

        private void OnLevelUpCommand(Unit _) => LevelUp();

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}
