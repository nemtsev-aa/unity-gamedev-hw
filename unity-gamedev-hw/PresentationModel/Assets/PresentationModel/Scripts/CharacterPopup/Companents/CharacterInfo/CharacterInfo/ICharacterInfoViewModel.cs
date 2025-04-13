using R3;
using System;
using System.Collections.Generic;

namespace PresentationModel {
    public interface ICharacterInfoViewModel : IViewModel, IDisposable {
        List<ICharacterStatViewModel> StatViewModels { get; }
        Observable<Unit> OnAnyStatChanged { get; }
    }
}