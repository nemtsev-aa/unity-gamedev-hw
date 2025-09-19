using MBT;
using UnityEngine;
using FarmingSystem;
using BehaviorTree.PlayerCompanents;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(ResourceSpotSensor))]
    public class ResourceSpotSensor : Service {
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
            _mask = _collector.ResourceMask;

            _isEntered = true;
        }

        public override void Task() {

            if (_isEntered == false)
                OnEnter();

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: task</color>");

            Collider[] colliders = Physics.OverlapSphere(BotPosition, _range, _mask, QueryTriggerInteraction.Ignore);

            if (colliders.Length > 0) {
                FindNearestSource(colliders);

                if (_showDebugMessage.Value == true)
                    Debug.Log($"<color=green> {title}: succes!</color>");
            }

            if (_showDebugMessage.Value == true)
                Debug.LogError($"{title}: Colliders not found!");
        }

        private void FindNearestSource(Collider[] colliders) {

            if (_showDebugMessage.Value == true)
                Debug.Log($"{title}: Find [{colliders.Length}] colliders");

            _botBrainData.SwitchBotState(BotStates.DetectResourceSource);

            Transform nearestTransform = null;
            ResourceSpot nearestResourceSource = null;

            float minDist = float.MaxValue;

            foreach (var iCollider in colliders) {

                if (iCollider.TryGetComponent(out ResourceSpotProxy proxy) == false)
                    continue;

                if (proxy.Source.IsActive == false)
                    continue;

                var source = proxy.Source;
                var sourceTransform = source.transform;
                var sDist = (BotPosition - sourceTransform.position).sqrMagnitude;

                if (sDist < minDist) {
                    minDist = sDist;
                    nearestTransform = sourceTransform;
                    nearestResourceSource = source;
                }
            }

            _botBrainData.SetResourceSource(nearestResourceSource);

            if (_showDebugMessage.Value == true)
                Debug.Log($"{title}: NearestSource [{nearestResourceSource.name}]. Distance [{minDist}]!");
        }
    }
}
