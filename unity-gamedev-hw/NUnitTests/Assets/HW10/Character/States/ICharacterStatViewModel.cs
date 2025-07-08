using R3;
using System;

namespace Character.UI {
    public interface ICharacterStatViewModel : IViewModel, IDisposable {
        string Name { get; }
        ReadOnlyReactiveProperty<int> Value { get; }
    }
}