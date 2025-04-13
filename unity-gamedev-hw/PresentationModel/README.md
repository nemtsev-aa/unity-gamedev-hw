# [Presentation Model]

Паттерн применён для реализации окна пользовательского интерфейса:
 - выполнена вёрстка CharacterPopup по образцу;
 - модели данных представлены классами UserInfo, PlayerLevel, CharacterInfo, CharacterStat;
 - каждой модели данных соответствует отдельная ViewModel (UserInfoViewModel, PlayerLevelViewModel, CharacterInfoViewModel, CharacterStatViewModel;
 - каждой модели данных соответствует отдельная View (UserInfoView, PlayerLevelView, CharacterInfoView, CharacterStatView;
 - доступ к Config-файлам реализован с помощью DataConfigProvider;
 - для создания ViewModel из Config-файлов используется CharacterPopupViewModelsFactory;
 - динамическая инициализация CharacterPopup осуществляется с помощью CharacterPopupPresenter и CharacterPopupPresenterView;
 - динамическое изменение CharacterPopup осуществляется с помощью RealTimeChangeManager;
 - использованы реактивные свойства и команды (R3);

## Демонстрация: https://cloud.mail.ru/public/zaw6/j6Kt3UZpr