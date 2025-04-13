using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace PresentationModel {
    public sealed class UserInfoChanger : MonoBehaviour, ICharacterInfoChanger {
        [SerializeField] private string _newUserName;
        [SerializeField] private string _newDescription;
        [SerializeField] private Sprite _newIcon;

        private UserInfo _userInfo;
        public UserInfoView _userInfoView { get; private set; }

        public void Init(ICharacterPopupViewModel viewModel, UIView uIView) {

            if (uIView is not UserInfoView userInfoView)
                throw new ArgumentNullException($"Invalid UIView: {uIView}");

            _userInfoView = (UserInfoView)uIView;

            _userInfo = viewModel.UserInfoViewModel.UserInfo;
        }

        [Button]
        public void SetUserName() {
            _userInfo.ChangeUserName(_newUserName);
            _userInfoView.UpdateCompanents();
        }

        [Button]
        public void SetDescription() {
            _userInfo.ChangeDescription(_newDescription);
            _userInfoView.UpdateCompanents();
        }

        [Button]
        public void SetIcon() {
            _userInfo.ChangeIcon(_newIcon);
            _userInfoView.UpdateCompanents();
        }
    }
}


