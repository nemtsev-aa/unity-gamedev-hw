using System.Collections.Generic;

namespace Tutorial.Core {

    public sealed class TutorialControllersProvider {
        public Dictionary<TutorialStep, TutorialStateControllerBase> Controllers { get; private set; }

        public TutorialControllersProvider(TutorialMainController main,
                                           ShowStartPopupStepController showStartPopup,
                                           TakeResourceStepController takeResource,
                                           CollectLootStepController collectLoot,
                                           SellResourceStepController sellResource,
                                           CharacterUpgradeStepController characterUpgrade,
                                           KillEnemyStepController killEnemy,
                                           ShowFinishPopupStepController showFinishPopup) {

            Controllers = new Dictionary<TutorialStep, TutorialStateControllerBase>() {
                { TutorialStep.Start, main},
                { TutorialStep.Welcome, showStartPopup},
                { TutorialStep.TakeResource, takeResource},
                { TutorialStep.CollectLoot, collectLoot},
                { TutorialStep.SellResource, sellResource},
                { TutorialStep.UpgradeCharacters, characterUpgrade},
                { TutorialStep.KillEnemy, killEnemy},
                { TutorialStep.Congratulate, showFinishPopup},
                { TutorialStep.End, null }
            };
        }
    }
}

