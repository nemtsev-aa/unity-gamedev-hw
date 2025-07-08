using R3;
using Character.Core;

namespace Character.UI {
    public sealed class CharacterStatViewModel : ICharacterStatViewModel {
        private readonly ReactiveProperty<int> _value = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public string Name { get; private set; }
        public ReadOnlyReactiveProperty<int> Value { get; }

        public CharacterStatViewModel(CharacterStat characterStat) {
            Name = characterStat.Name;
            _value.Value = characterStat.Value.CurrentValue;
            Value = _value.ToReadOnlyReactiveProperty();

            CreateSubscribes(characterStat);
        }

        private void CreateSubscribes(CharacterStat characterStat) {
            characterStat.Value
                .Subscribe(OnValueChange)
                .AddTo(_compositeDisposable);
        }

        private void OnValueChange(int newValue) {
            _value.Value = newValue;
        }

        public void Dispose() {

            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}