using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace PresentationModel {
    public sealed partial class PlayerLevelChanger : MonoBehaviour, ICharacterInfoChanger {
        [Range(10, 100)][SerializeField] private int _level;
        [Range(10, 100)][SerializeField] private int _exp;

        private PlayerLevelView _playerLevelView;
        private PlayerLevel _playerLevel;

        public void Init(ICharacterPopupViewModel viewModel, UIView uIView) {

            if (uIView is not PlayerLevelView playerLevelView)
                throw new ArgumentNullException($"Invalid UIView: {uIView}");

            _playerLevelView = (PlayerLevelView)uIView;
            _playerLevel = viewModel.PlayerLevelViewModel.PlayerLevel;
        }

        [Button]
        public void SetLevel() {
            _playerLevel.SetLevel(_level);
            _playerLevelView.UpdateCompanents();
        }

        [Button]
        public void AddExp() {
            _playerLevel.AddExperience(_exp);
            _playerLevelView.UpdateCompanents();
        }
    }
}
