using System;
using UnityEngine;
using Zenject;

namespace Tutorial.UI {

    [Serializable]
    public sealed class TutorialUIInstaller {
        [Space, SerializeField] private TutorialStepInfoView _tutorialStepInfoView;

        [Header("Popups")]
        [SerializeField] private TutorialStartPopup _startPopup;
        [SerializeField] private TutorialCharacterUpgradePopup _characterUpgradePopup;
        [SerializeField] private TutorialFinishPopup _finishPopup;

        public void Install(DiContainer container) {
            var config = container.Resolve<TutorialStepInfoConfig>();

            var viewModel = new TutorialStepInfoViewModel(config);
            container.BindInstance(viewModel)
                     .AsSingle()
                     .NonLazy();

            _tutorialStepInfoView.Init(viewModel);
            container.BindInstance(_tutorialStepInfoView)
                     .AsSingle()
                     .NonLazy();

            container.BindInstance(_startPopup)
                     .AsSingle()
                     .NonLazy();

            container.BindInstance(_characterUpgradePopup)
                     .AsSingle()
                     .NonLazy();

            container.BindInstance(_finishPopup)
                     .AsSingle()
                     .NonLazy();
        }
    }
}

