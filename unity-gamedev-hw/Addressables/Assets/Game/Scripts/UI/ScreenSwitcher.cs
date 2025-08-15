using System.Collections.Generic;
using UI.Components.Screens;
using UI.Core;

namespace UI.Services {

    public sealed class ScreenSwitcher {
        private readonly UIManager _uiManager;
        private readonly Stack<UIScreenType> _screenHistory = new();

        public ScreenSwitcher(UIManager uiManager) {
            _uiManager = uiManager;
        }

        public void AddToHistory(UIScreenType screenType) {

            if (_screenHistory.Contains(screenType) == false)
                _screenHistory.Push(screenType);
        }

        public void RemoveFromHistory(UIScreenType screenType) {
            var tempStack = new Stack<UIScreenType>();

            while (_screenHistory.Count > 0) {
                var screen = _screenHistory.Pop();

                if (screen != screenType)
                    tempStack.Push(screen);
            }

            while (tempStack.Count > 0) {
                _screenHistory.Push(tempStack.Pop());
            }
        }

        public bool TryGetCurrentScreen(out UIScreenType screenType) {
            
            if (_screenHistory.Count > 0 && _uiManager.IsScreenAvailable(_screenHistory.Peek())) {
                screenType = _screenHistory.Peek();
                return true;
            }

            screenType = default;
            return false;
        }


        public bool TryShowPreviousScreen(out UIScreenType? screen) {

            if (_screenHistory.Count == 0) {
                screen = null;
                return false;
            }

            var current = _screenHistory.Pop();

            while (_screenHistory.Count > 0) {
                var candidate = _screenHistory.Pop();

                if (_uiManager.IsScreenAvailable(candidate) == true) {
                    _screenHistory.Push(candidate);

                    screen = candidate;
                    return true;
                }
            }

            _screenHistory.Push(current);

            screen = current;
            return false;
        }
    }
}
