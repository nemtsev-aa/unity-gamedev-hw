using System;

namespace GameCycleSystem {

    [Serializable]
    public sealed class GameCycle_UI {
        private readonly GameCycleView _gameCycleView;

        public GameCycle_UI(GameCycleView gameCycleView) {
            _gameCycleView = gameCycleView;
        }

        public void ShowGameCycleView() {
            var status = !_gameCycleView.gameObject.activeSelf;
            _gameCycleView.Show(status);
        }
    }
}



