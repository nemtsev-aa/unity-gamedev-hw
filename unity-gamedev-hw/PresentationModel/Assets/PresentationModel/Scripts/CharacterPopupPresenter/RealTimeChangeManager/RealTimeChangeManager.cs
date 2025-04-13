using UnityEngine;

namespace PresentationModel {
    public sealed class RealTimeChangeManager : MonoBehaviour {
        [SerializeField] private UserInfoChanger _userInfoChanger;
        [SerializeField] private PlayerLevelChanger _playerLevelChanger;
        [SerializeField] private CharacterInfoChanger _characterInfoChanger;

        public void Init(CharacterPopup characterPopup) {
              var viewModel = characterPopup.ViewModel;

            _userInfoChanger.Init(viewModel, characterPopup.UserInfoView);
            _playerLevelChanger.Init(viewModel, characterPopup.PlayerLevelView);
            _characterInfoChanger.Init(viewModel, characterPopup.CharacterInfoView);
        }
    }
}


