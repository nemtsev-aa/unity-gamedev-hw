using System;


namespace PresentationModel {
    public interface ICharacterPopupViewModel : IViewModel {
        UserInfoViewModel UserInfoViewModel { get; }
        PlayerLevelViewModel PlayerLevelViewModel { get; }
        CharacterInfoViewModel CharacterInfoViewModel { get; }
    }
}


