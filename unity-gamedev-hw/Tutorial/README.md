# Обучающий уровень для RPG [Tutorial]

## Техническое задание:

https://cloud.mail.ru/public/cazy/FTKqqEUiM

## Особенности реализации:

- Процесс внедрения зависимостей реализован с помощью **Zenject**.
- Взаимодействие классов и систем реализовано с помощью реактивного программирования (библиотека **R3**). 
- Асинхронные операции реализованы c помощью **UniTask**.
- Анимации реализованы c помощью **DoTween**.

## Основные системы:

- **InputService** - сервис, отслежавающий ввод пользователя. Содержит функцию блокировки ввода.
- **InteractionService** - сервис, позволяющий организовать взаимодействие игрока с предметами и NPC.
- **NavigatorService** - сервис, отображающий направление движения до текущей цели.
- **PlayerUpgrades** - система улучшений персонажа. Позволяет вести гибкую настройку стоимости и прогрессии улучшений через Config-файлы (*PlayerDamageUpgradeConfig*, *PlayerHealthUpgradeConfig*, *PlayerSpeedUpgradeConfig*). Взаимодействие частей системы реализовано на основе паттерна MVVM.
- **ProgressService** - сервис регистрации прогресса игрока и текущего шага обучения.
- **SaveSystem** - система сохранения данных. Реализована возможность локального сохранения данных в JSON и Bin-форматах. 
- **CursorChangerService** - сервис изменения внешнего вида указателя мыши.
- **HintPlayerControlService** - сервис подсказок управления игроком.
- **TutorialCore** - ядро модуля, объединяющее в себе *TutorialEntryPoint*, *TutorialStateRunner*, *TutorialState*. Каждый шаг обучения управляется с помощью отдельного контроллера (*ShowStartPopupStepController*, *TakeResourceStepController* и т.д). Настройка контроллеров осуществляется с помощью соответствующих Config-фалов.
- **Player** - модель игрока переиспользована (см. https://github.com/nemtsev-aa/unity-gamedev-hw/tree/BehaviorTree/unity-gamedev-hw/BehaviorTree#бот-лесоруб-behaviortree) 


### Демонстрация: https://cloud.mail.ru/public/KYUK/JTL2RJi3W