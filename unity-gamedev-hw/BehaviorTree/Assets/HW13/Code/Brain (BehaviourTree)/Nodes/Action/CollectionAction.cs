using MBT;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(CollectionAction))]
    public class CollectionAction : Leaf {

        [SerializeField] private float _updateInterval = 1f;
        [SerializeField] private IntReference _cargoQuantity;
        [Space, SerializeField] private bool _result;

        private float _time = 0;

        public override void OnEnter() {
            _time = 0;
        }

        public override NodeResult Execute() {
            Debug.Log($"<color=green> Execute {title} </color>");

            _time += Time.deltaTime;

            if (_time > _updateInterval) {
                _time = 0;

                if (_result == false) {
                    Debug.Log($"<color=blue> Fail CargoQuantity </color>");
                    return NodeResult.failure;
                }

                _cargoQuantity.Value++;
                Debug.Log($"<color=blue> Add CargoQuantity </color>");
                return NodeResult.success; 
            }

            Debug.Log($"<color=red> Running </color>");
            return NodeResult.running;
        }
    }
}
