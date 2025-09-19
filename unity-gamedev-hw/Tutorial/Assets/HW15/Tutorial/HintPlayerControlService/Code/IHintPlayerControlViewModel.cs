using R3;

namespace HintPlayerControlService {
    public interface IHintPlayerControlViewModel {
        ReadOnlyReactiveProperty<HintPlayerControlConfig> CurrentData { get; }

    }
}