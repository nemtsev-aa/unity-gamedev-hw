using R3;
using Sirenix.OdinInspector;
using System;

namespace Character.Core {

    [Serializable]
    public sealed class CharacterStat {
        private readonly string _name;
        private readonly ReactiveProperty<int> _value = new ReactiveProperty<int>();

        public CharacterStat(string name, int value) {
            _name = name;
            _value.Value = value;
        }

        [ShowInInspector] public string Name => _name;
        [ShowInInspector] public int CurrentValue => _value.CurrentValue;
        public ReactiveProperty<int> Value => _value;

        public void ChangeValue(int value) {
            _value.Value = value;
        }
    }
}