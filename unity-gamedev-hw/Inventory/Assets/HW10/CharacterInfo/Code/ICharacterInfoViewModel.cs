using R3;
using System;
using System.Collections.Generic;
using Character.UI;

namespace CharacterInfoWidget {

    public interface ICharacterInfoViewModel : IViewModel, IDisposable {
        List<ICharacterStatViewModel> StatViewModels { get; }
        Observable<Unit> OnAnyStatChanged { get; }
    }
}