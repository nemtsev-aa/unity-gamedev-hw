using GameCycleSystem;

namespace StepByStepButler.Gameplay.Systems {

    public interface IGameSystem : IGameInitializeListener,
                                   IGameFinishListener,
                                   IGameRestartListener {

    }
}