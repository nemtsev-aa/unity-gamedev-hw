using Zenject;
using UnityEngine;
using Tutorial.UI;
using Characters;

namespace Tutorial.Core {

    public sealed class TutorialInstaller : MonoInstaller {
        [SerializeField] private TutorialStepInfoConfig _config;
        [Header("UI Components")]
        [SerializeField] private TutorialUIInstaller _ui;
        [Header("State Settings")]
        [SerializeField] private TutorialControllersInstaller _controllers;
        [Header("Characters Settings")]
        [Space, SerializeField] private CharacterProvider _characterProvider;

        public override void InstallBindings() {

            Container.BindInstance(_config)
                     .AsSingle()
                     .NonLazy();

            _ui.Install(Container);
            _controllers.Install(Container);

            Container.Bind<TutorialStateRunner>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}