using UnityEngine;

namespace InteractionService {
    public class Interaction {
        private InteractionTypes _interactionType;
        private float _duration;
        private float _time;
        private float _currentPercent;

        public float CurrentPercent => _currentPercent;

        public Interaction(InteractionTypes interactionType, float duration = 1) {
            _interactionType = interactionType;
            _duration = duration;
            _time = _duration;
        }

        public void Update() {
            _time -= Time.deltaTime;

            if (_time > 0)
                _currentPercent = (_time / _duration);
            else
                _currentPercent = 1f;

            //Debug.Log($"Interaction: {_currentPercent}");
        }
    }
}

