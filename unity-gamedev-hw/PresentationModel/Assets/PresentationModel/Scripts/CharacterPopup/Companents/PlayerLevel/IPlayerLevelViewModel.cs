using R3;
using System;

namespace PresentationModel {
    public interface IPlayerLevelViewModel : IViewModel, IDisposable {
        int Level { get; }
        int Experience { get; }
        int RequiredExperience { get; }

        ReactiveProperty<bool> CanLevelUp { get; }
        ReactiveCommand LevelUpCommand { get; }

        void LevelUp();
    }
}
