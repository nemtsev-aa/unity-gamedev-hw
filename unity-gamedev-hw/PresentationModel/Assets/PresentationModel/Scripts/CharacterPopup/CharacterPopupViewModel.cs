namespace PresentationModel {
    public sealed class CharacterPopupViewModel : ICharacterPopupViewModel {
        public UserInfoViewModel UserInfoViewModel { get; private set; }
        public PlayerLevelViewModel PlayerLevelViewModel { get; private set; }
        public CharacterInfoViewModel CharacterInfoViewModel { get; private set; }

        public CharacterPopupViewModel(UserInfoViewModel userInfoViewModel, PlayerLevelViewModel playerLevelViewModel, CharacterInfoViewModel characterInfoViewModel) {
            UserInfoViewModel = userInfoViewModel;
            PlayerLevelViewModel = playerLevelViewModel;
            CharacterInfoViewModel = characterInfoViewModel;
        }
    }
}


