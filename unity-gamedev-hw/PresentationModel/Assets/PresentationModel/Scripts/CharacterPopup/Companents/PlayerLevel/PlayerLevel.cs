using R3;
using System;

namespace PresentationModel {
    public sealed class PlayerLevel {
        private readonly ReactiveProperty<int> _level = new();
        private readonly ReactiveProperty<int> _experience = new();

        public PlayerLevel(int currentLevel, int currentExperience) {
            _level.Value = currentLevel;
            _experience.Value = currentExperience;
        }

        public ReadOnlyReactiveProperty<int> CurrentLevel => _level;
        public ReadOnlyReactiveProperty<int> CurrentExperience => _experience; 

        public int RequiredExperience {
            get { return 100 * (_level.Value + 1); }
        }

        public void SetLevel(int value) {
            _level.Value = value;
        }

        public bool CanLevelUp() {
            return _experience.Value == RequiredExperience;
        }

        public void AddExperience(int range) { 
            _experience.Value = Math.Min(_experience.Value + range, RequiredExperience);
        }

        public void LevelUp() {
            
            if (CanLevelUp() == true) {
                _experience.Value = 0;
                _level.Value++;
            }
        }
    }
}
