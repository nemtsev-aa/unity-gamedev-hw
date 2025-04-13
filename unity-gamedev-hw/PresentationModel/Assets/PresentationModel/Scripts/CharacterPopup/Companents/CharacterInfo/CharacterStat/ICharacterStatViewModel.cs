using R3;
using System;

namespace PresentationModel {
    public interface ICharacterStatViewModel : IViewModel, IDisposable {
        string Name { get; }
        ReadOnlyReactiveProperty<int> Value { get; }
    }
}