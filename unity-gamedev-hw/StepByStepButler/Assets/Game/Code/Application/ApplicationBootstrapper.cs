using Zenject;
using UnityEngine;
using UI.Core;
using UI.Components.Screens;

namespace StepByStepButler.Core {

    public sealed class ApplicationBootstrapper : MonoBehaviour {

        private UIManager _uiManager;

        [Inject]
        public void Construct(UIManager uiManager) {
            _uiManager = uiManager;
        }

        private async void Start() {
            await _uiManager.ShowScreenAsync<MenuScreen>(UIScreenType.MenuScreen);
        }
    }
}
