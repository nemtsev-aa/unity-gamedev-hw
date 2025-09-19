using Tutorial.Core;
using UnityEngine;

namespace Tutorial.UI {

    [CreateAssetMenu(
        fileName = nameof(TutorialStepInfo),
        menuName = "Tutorial/" + nameof(TutorialStepInfo)
    )]
    public sealed class TutorialStepInfo : ScriptableObject {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public TutorialStep Type { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}

