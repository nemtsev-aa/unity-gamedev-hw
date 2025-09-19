using MBT;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(SetPatrolPointAction))]
    public class SetPatrolPointAction : Leaf {
        [SerializeField] private BotBrainDataReference _data;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private int _index;
        private int _direction = 1;

        private BotBrainData _botBrainData => _data.Value;
        private IReadOnlyList<Transform> _waypoints => _data.Value.Waypoints;

        public override NodeResult Execute() {

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: execute </color>");

            if (_waypoints.Count == 0)
                return NodeResult.failure;

            if (_direction == 1 && _index == _waypoints.Count - 1)
                _direction = -1;
            else if (_direction == -1 && _index == 0)
                _direction = 1;

            _index += _direction;

            _botBrainData.SetCurrentPatrolPoint(_waypoints[_index]);

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=green> {title}: success </color>");

            return NodeResult.success;
        }
    }
}

