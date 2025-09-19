using MBT;
using Zenject;
using UnityEngine;
using BehaviorTree.Bot;
using FarmingSystem;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(DistanceCondition))]
    public class DistanceCondition : Condition {
        [SerializeField] private Comparator comparator = Comparator.GreaterThan;
        [SerializeField] private float distance = 5f;
        [Space, SerializeField] private bool _showDebugMessage = false;

        private BotModel _bot;
        private BotBrainData _brainData;
        private bool _result;

        private Transform _botTransform => _bot.Player.transform;
        private FellingZone _zone => _brainData.NearestFarmingZone.CurrentValue;

        [Inject]
        public void Construct(BotModel bot) {
            _bot = bot;
            _brainData = _bot.BrainData;
        }

        public override bool Check() {

            if (_zone == null) {

                if (_showDebugMessage == true)
                    Debug.Log($"<color=red> {title}: FarmingZone not found </color>");

                return false;
            }

            float sqrMagnitude = (_zone.transform.position - _botTransform.position).sqrMagnitude;

            if (comparator == Comparator.GreaterThan)
                _result = sqrMagnitude > distance * distance;
            else
                _result = sqrMagnitude < distance * distance;

            if (_showDebugMessage == true)
                Debug.Log($"<color=yellow> {title}: [{sqrMagnitude}] CheckResult [{_result}] </color>");

            return _result;
        }

        public enum Comparator {
            GreaterThan,
            LessThan
        }
    }
}