using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace UpgradesSystem.ConveyorUpgrades {

    [Serializable]
    public sealed class UpdateValuesTable {
        [InfoBox("Linear Function")]
        [Space, SerializeField] private float _startValue;
        [SerializeField] private float _endValue;
        [ReadOnly, SerializeField] private float _step;

        [ListDrawerSettings(
            IsReadOnly = true,
            OnBeginListElementGUI = "DrawLabelForListElement"
        )]
        [Space, SerializeField]
        private float[] _table;

        public float Step => _step;

        public float GetLevel(float value) {

            for (int i = 0; i < _table.Length; i++) {

                if (_table[i] == value)
                    return i;
            }

            return 0;
        }

        public float GetValue(int level) {
            var index = Mathf.Clamp(level - 1, 0, _table.Length);
            return _table[index];
        }

        public void OnValidate(int maxLevel) {
            EvaluateTable(maxLevel);
        }

        private void EvaluateTable(int maxLevel) {
            _table = new float[maxLevel];
            _table[0] = _startValue;
            _table[maxLevel - 1] = _endValue;

            var speedStep = (_endValue - _startValue) / (maxLevel - 1);
            _step = (float)Math.Round(speedStep, 2);

            for (var i = 1; i < maxLevel - 1; i++) {
                var speed = _startValue + _step * i;
                _table[i] = (float)Math.Round(speed, 2);
            }
        }

#if UNITY_EDITOR
        private void DrawLabelForListElement(int index) {
            GUILayout.Space(8);
            GUILayout.Label($"Level {index + 1}");
        }
#endif
    }
}