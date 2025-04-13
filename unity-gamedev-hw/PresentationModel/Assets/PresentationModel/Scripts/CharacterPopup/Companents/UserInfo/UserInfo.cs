using R3;
using UnityEngine;

namespace PresentationModel {
    public sealed class UserInfo {
        private readonly ReactiveProperty<string> _userName = new ReactiveProperty<string>();
        private readonly ReactiveProperty<string> _description = new ReactiveProperty<string>();
        private readonly ReactiveProperty<Sprite> _icon = new ReactiveProperty<Sprite>();

        public ReadOnlyReactiveProperty<string> UserName => _userName;
        public ReadOnlyReactiveProperty<string> Description => _description;
        public ReadOnlyReactiveProperty<Sprite> Icon => _icon;

        public UserInfo(string userName, string description, Sprite icon) {
            _userName.Value = userName;
            _description.Value = description;
            _icon.Value = icon;
        }

        public void ChangeUserName(string userName) {
            _userName.Value = userName;
        }

        public void ChangeDescription(string description) {
            _description.Value = description;
        }

        public void ChangeIcon(Sprite icon) {
            _icon.Value = icon;
        }
    }
}