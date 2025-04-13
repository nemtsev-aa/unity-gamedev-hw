using R3;
using UnityEngine;

namespace PresentationModel {
    public sealed class UserInfoViewModel : IUserInfoViewModel {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        public UserInfo UserInfo { get; private set; }
        public string UserName { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }

        public UserInfoViewModel(UserInfo userInfo) {
            UserInfo = userInfo;

            UserName = UserInfo.UserName.CurrentValue;
            Description = UserInfo.Description.CurrentValue; 
            Icon = UserInfo.Icon.CurrentValue;

            CreateRactiveSubscribes(userInfo);
        }

        private void CreateRactiveSubscribes(UserInfo userInfo) {
            userInfo.UserName
                .Subscribe(OnUserNameChange)
                .AddTo(_compositeDisposable);

            userInfo.Description
                .Subscribe(OnDescriptionChange)
                .AddTo(_compositeDisposable);

            userInfo.Icon
                .Subscribe(OnIconChange)
                .AddTo(_compositeDisposable);
        }

        private void OnUserNameChange(string userName) {
            UserName = userName;
        }

        private void OnDescriptionChange(string description) {
            Description = description;
        }

        private void OnIconChange(Sprite icon) {
            Icon = icon;
        }

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}