using FarmingSystem;
using MBT;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(FarmingZoneSensor))]
    public class FarmingZoneSensor : Service {
        [SerializeField] private BotBrainDataReference _data;
        [Space, SerializeField] private BoolReference _showDebugMessage;
        private Vector3 _botPosition;

        private BotBrainData _botBrainData => _data.Value;
        private IReadOnlyList<FellingZone> _fellingZones => _data.Value.FellingZones;

        public override void Task() {

            if (CheckCurrentFarmingZone() == true) {

                if (_showDebugMessage.Value == true)
                    Debug.Log($"<color=green> {title}: CurrentFarmingZone is relevant </color>");

                return;
            }

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: task </color>");

            if (TryFindNearestFarmingZone(out FellingZone zone) == true) {
                _botBrainData.SetFarmingZone(zone);

                if (_showDebugMessage.Value == true)
                    Debug.Log($"<color=green> {title}: succes </color>");
            }
        }

        private bool TryFindNearestFarmingZone(out FellingZone zone) {
            _botBrainData.SwitchBotState(BotStates.DetectFarmingZone);

            FellingZone currentZone = null;
            float minDist = float.MaxValue;
            _botPosition = _botBrainData.Root.transform.position;

            foreach (var iZone in _fellingZones) {

                if (iZone.TreesAmount == 0 && iZone.LootAmount == 0)
                    continue;

                float currentDist = (_botPosition - iZone.transform.position).sqrMagnitude;

                if (currentDist < minDist) {
                    minDist = currentDist;
                    currentZone = iZone;
                }
            }

            if (currentZone != null) {

                if (currentZone.TreesAmount > 0 || currentZone.LootAmount > 0) {

                    if (currentZone.LootAmount > 0)
                        _botBrainData.SwitchWorkOperationPriority(WorkOperationPriority.Collecting);
                    else
                        _botBrainData.SwitchWorkOperationPriority(WorkOperationPriority.Farming);
                }

                zone = currentZone;
                return true;
            }

            zone = null;
            return false;
        }

        private bool CheckCurrentFarmingZone() {
            var currentZone = _botBrainData.NearestFarmingZone.CurrentValue;

            if (currentZone == null)
                return false;

            if (currentZone.TreesAmount == 0 && currentZone.LootAmount == 0)
                return false;

            return true;
        }
    }
}

