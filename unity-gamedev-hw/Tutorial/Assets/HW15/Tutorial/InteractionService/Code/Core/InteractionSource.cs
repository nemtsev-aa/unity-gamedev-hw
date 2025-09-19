using System;
using UnityEngine;

namespace InteractionService {
    public class InteractionSource : MonoBehaviour {
        public event Action<float> PercentageCompletionChanged;

        [field: SerializeField] public InteractionConfig Config { get; private set; }

        public Interaction Interaction { get; private set; }
        public bool IsActive { get; private set; } = false;

        private void Start() {
            if (Config == null)
                throw new ArgumentNullException($"InteractionConfig not found");

            Interaction = new Interaction(Config.InteractionType);
        }

        public virtual void Activate() {
            IsActive = true;
        }

        private void Update() {
            if (IsActive == false)
                return;

            Interaction.Update();

            PercentageCompletionChanged?.Invoke(Interaction.CurrentPercent);

            if (Interaction.CurrentPercent == 1) {
                IsActive = false;
                //Debug.Log("Interaction complited!");
            }
        }
    }
}
