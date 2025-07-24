using MBT;
using UnityEngine;
using FarmingSystem;
using BehaviorTree.PlayerCompanents;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(LootSensor))]
    public class LootSensor : Service {
        [SerializeField] private BotBrainDataReference _dataReference;
        [SerializeField] private PlayerReference _playerReference;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private Vector3 BotPosition => _playerReference.Value.transform.position;

        private BotBrainData _botBrainData;
        private CollectorCompanent _collector;
        private float _range;
        private LayerMask _mask;
        private bool _isEntered;

        public override void OnEnter() {

            if (_isEntered == true)
                return;

            _botBrainData = _dataReference.Value;
            _collector = _playerReference.Value.Core.Collector;

            _range = _collector.DistanceToCollect;
            _mask = _collector.LootMask;

            _isEntered = true;
        }

        public override void Task() {

            if (_isEntered == false)
                OnEnter();

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: task </color>");

            Collider[] colliders = Physics.OverlapSphere(BotPosition, _range, _mask, QueryTriggerInteraction.Ignore);

            if (colliders.Length == 0) {
                ClearDetection();
                return;
            }

            FindNearestLoot(colliders);
        }

        private void FindNearestLoot(Collider[] colliders) {
            _botBrainData.SwitchBotState(BotStates.DetectResourceLoot);

            ResourceLoot nearestLoot = null;
            float minDist = float.MaxValue;

            foreach (var col in colliders) {

                if (col.TryGetComponent<LootProxy>(out var proxy) == false)
                    continue;

                var dist = Vector3.Distance(BotPosition, proxy.transform.position);

                if (dist < minDist) {
                    minDist = dist;
                    nearestLoot = proxy.Loot as ResourceLoot;
                }
            }

            if (nearestLoot != null) {
                _botBrainData.SetResourceLoot(nearestLoot);
                return;
            }

            ClearDetection();
        }

        private void ClearDetection() {
            _dataReference.Value.SetResourceLoot(null);
        }
    }
}
