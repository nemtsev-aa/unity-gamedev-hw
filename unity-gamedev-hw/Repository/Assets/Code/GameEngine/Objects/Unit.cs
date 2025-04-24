using UnityEngine;

namespace GameEngine {
    //Нельзя менять!
    public sealed class Unit : MonoBehaviour {
        public string Type {
            get => _type;
        }

        public int HitPoints {
            get => _hitPoints;
            set => _hitPoints = value;
        }

        public Vector3 Position {
            get => transform.position;
        }

        public Vector3 Rotation {
            get => transform.eulerAngles;
        }

        [SerializeField]
        private string _type;

        [SerializeField]
        private int _hitPoints;

        private void Reset() {
            _type = name;
            _hitPoints = 10;
        }
    }
}