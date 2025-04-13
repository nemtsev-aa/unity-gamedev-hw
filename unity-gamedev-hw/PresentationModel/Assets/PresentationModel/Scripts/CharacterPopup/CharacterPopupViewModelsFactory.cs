namespace PresentationModel {
    public sealed class CharacterPopupViewModelsFactory {
        private readonly DataConfigProvider _configProvider;

        public CharacterPopupViewModelsFactory(DataConfigProvider configProvider) {
            _configProvider = configProvider;
        }

        public bool GetViewModel<T>(out T viewModel) where T : IViewModel {
            viewModel = default;

            if (typeof(T) == typeof(UserInfoViewModel)) {
                viewModel = (T)(IViewModel)GetUserInfoViewModel();
                return true;
            }

            if (typeof(T) == typeof(PlayerLevelViewModel)) {
                viewModel = (T)(IViewModel)GetPlayerLevelViewModel();
                return true;
            }

            if (typeof(T) == typeof(CharacterInfoViewModel)) {
                viewModel = (T)(IViewModel)GetCharacterInfoViewModel();
                return true;
            }

            if (typeof(T) == typeof(CharacterPopupConfig)) {
                viewModel = (T)(IViewModel)GetCharacterPopupViewModel();
                return true;
            }

            return false;
        }

        public CharacterPopupViewModel Create(CharacterPopupConfig config = null) {

            if (config == null) {
                return GetCharacterPopupViewModel();
            }

            return GetCharacterPopupViewModel(config);
        }

        #region Configs From Provider
        private UserInfoViewModel GetUserInfoViewModel() {
            if (!_configProvider.TryGetConfig<UserInfoConfig>(out var config))
                throw new ConfigNotFoundException(typeof(UserInfoConfig));

            return new UserInfoViewModel(new UserInfo(config.UserName, config.Description, config.Icon));
        }

        private PlayerLevelViewModel GetPlayerLevelViewModel() {
            if (!_configProvider.TryGetConfig<PlayerLevelConfig>(out var config))
                throw new ConfigNotFoundException(typeof(PlayerLevelConfig));

            return new PlayerLevelViewModel(new PlayerLevel(config.Level, config.Experience));
        }

        private CharacterInfoViewModel GetCharacterInfoViewModel() {
            if (!_configProvider.TryGetConfig<CharacterStatConfigs>(out var config))
                throw new ConfigNotFoundException(typeof(CharacterStatConfigs));

            return new CharacterInfoViewModel(new CharacterInfo(config));
        }

        private CharacterPopupViewModel GetCharacterPopupViewModel() {
            if (!_configProvider.TryGetConfig<CharacterPopupConfig>(out var config))
                throw new ConfigNotFoundException(typeof(CharacterPopupConfig));

            return new CharacterPopupViewModel(
                GetUserInfoViewModel(),
                GetPlayerLevelViewModel(),
                GetCharacterInfoViewModel()
            );
        }
        #endregion

        #region RealTime Configs
        private UserInfoViewModel GetUserInfoViewModel(UserInfoConfig config) {
            if (config == null)
                throw new ConfigNotFoundException(typeof(UserInfoConfig));

            return new UserInfoViewModel(new UserInfo(config.UserName, config.Description, config.Icon));
        }

        private PlayerLevelViewModel GetPlayerLevelViewModel(PlayerLevelConfig config) {
            if (config == null)
                throw new ConfigNotFoundException(typeof(PlayerLevelConfig));

            return new PlayerLevelViewModel(new PlayerLevel(config.Level, config.Experience));
        }

        private CharacterInfoViewModel GetCharacterInfoViewModel(CharacterStatConfigs config) {
            if (config == null)
                throw new ConfigNotFoundException(typeof(CharacterStatConfigs));

            return new CharacterInfoViewModel(new CharacterInfo(config));
        }

        private CharacterPopupViewModel GetCharacterPopupViewModel(CharacterPopupConfig config) {
            if (config == null)
                throw new ConfigNotFoundException(typeof(CharacterPopupConfig));

            return new CharacterPopupViewModel(
                GetUserInfoViewModel(config.UserInfoConfig),
                GetPlayerLevelViewModel(config.PlayerLevelConfig),
                GetCharacterInfoViewModel(config.StatConfigs)
            );
        }
        #endregion

    }
}


