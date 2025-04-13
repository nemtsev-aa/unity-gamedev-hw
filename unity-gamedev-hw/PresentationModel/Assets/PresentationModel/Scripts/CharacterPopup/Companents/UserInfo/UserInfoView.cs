using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel {
    public class UserInfoView : UIView {
        [SerializeField] private TMP_Text _userNameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Image _iconImage;

        private IUserInfoViewModel _userInfoViewModel;

        public override void Init(IViewModel viewModel) {

            if (viewModel is not IUserInfoViewModel userInfoViewModel)
                throw new ArgumentNullException($"Invalid ViewModel: {viewModel}");

            _userInfoViewModel = userInfoViewModel;
            UpdateCompanents();
        }

        public override void UpdateCompanents() {
            _userNameText.text = $"{_userInfoViewModel.UserName}";
            _descriptionText.text = _userInfoViewModel.Description;
            _iconImage.sprite = _userInfoViewModel.Icon;
        }
    }
}