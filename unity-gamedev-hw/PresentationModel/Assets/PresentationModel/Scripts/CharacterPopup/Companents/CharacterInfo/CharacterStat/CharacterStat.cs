using R3;

namespace PresentationModel {
    public sealed class CharacterStat {
        private readonly string _name;
        private readonly ReactiveProperty<int> _value = new ReactiveProperty<int>();

        public CharacterStat(string name, int value) {
            _name = name;
            _value.Value = value;
        }

        public string Name => _name;
        public ReactiveProperty<int> Value => _value;

        public void ChangeValue(int value) {
            _value.Value = value;
        }
    }
}