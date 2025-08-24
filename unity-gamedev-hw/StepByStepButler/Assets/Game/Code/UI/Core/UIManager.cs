using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using UnityEngine;
using SceneManagementSystem;
using Cysharp.Threading.Tasks;
using UI.Components.Screens;
using UI.Services;
using UI.Components;

namespace UI.Core {

    public sealed class UIManager : MonoBehaviour {
        public ReadOnlyReactiveProperty<UIScreenType> ScreenOpened => _screenOpened;
        public ReadOnlyReactiveProperty<UIScreenType> ScreenClosed => _screenClosed;

        [SerializeField] private Transform[] _layerContainers;

        private readonly Dictionary<UIScreenType, UIScreenConfig> _configs = new();
        private readonly Dictionary<UIScreenType, UIComponent> _activeScreens = new();

        private ReactiveProperty<UIScreenType> _screenOpened = new();
        private ReactiveProperty<UIScreenType> _screenClosed = new();

        private ScreenFactory _screenFactory;
        private ScreenSwitcher _screenSwitcher;
        private UIScreenConfigs _screenConfigs;
        private bool _isInitialized;

        [Inject]
        public void Construct(UIScreenConfigs screenConfigs) {
            _screenConfigs = screenConfigs;
            _screenFactory = new ScreenFactory(_screenConfigs);
            _screenSwitcher = new ScreenSwitcher(this);

            _screenClosed = new();

            InitializeConfigs();
        }

        public async UniTask<T> ShowScreenAsync<T>(UIScreenType screenType, bool addToHistory = true, bool animated = true) where T : UIComponent {
            var config = _configs[screenType];

            if (config.AllowMultipleInstances == false)
                await HideScreensOnLayer(config.Layer, animated);

            if (_activeScreens.TryGetValue(screenType, out var activeScreen) == true) {
                await activeScreen.ShowAsync(animated);
                return activeScreen as T;
            }

            var parent = _layerContainers[(int)config.Layer / 100];
            var screen = _screenFactory.CreateScreen<T>(screenType, parent);

            await screen.ShowAsync(animated);

            _activeScreens[screenType] = screen;

            if (addToHistory == true)
                _screenSwitcher.AddToHistory(screenType);
            
            _screenOpened.Value = screenType;
            
            return screen;
        }

        public async UniTask HideScreenAsync(UIScreenType screenType, bool animated = true) {

            if (_activeScreens.TryGetValue(screenType, out var screen)) {
                await screen.HideAsync(animated);

                var config = _configs[screenType];

                if (config.DestroyOnHide == true) {
                    _activeScreens.Remove(screenType);    
                    Destroy(screen.gameObject);
                } 
                
                _screenSwitcher.RemoveFromHistory(screenType);
                _screenClosed.Value = screenType;
            }
        }

        public T GetActiveScreen<T>(UIScreenType screenType) where T : UIComponent {
            return _activeScreens.TryGetValue(screenType, out var screen) ? screen as T : null;
        }

        public async UniTask ShowPreviousScreenAsync(bool animated = true) {

            if (_screenSwitcher.TryGetCurrentScreen(out var activeScreen) == true)
                await HideScreenAsync(activeScreen);

            if (_screenSwitcher.TryShowPreviousScreen(out var previousScreenType) == false)
                return;

            if (previousScreenType.HasValue == true) {
                var config = _configs[previousScreenType.Value];
                
                await ShowScreenAsync<UIComponent>(previousScreenType.Value, addToHistory: false, animated: animated);
                return;
            }

            Debug.LogWarning("No previous screen in history");
        }

        public bool IsScreenAvailable(UIScreenType screenType) {

            if (_configs.TryGetValue(screenType, out var config) == false) {
                Debug.LogWarning($"No config found for screen type: {screenType}");
                return false;
            }

            if (_activeScreens.ContainsKey(screenType) == true) {
                Debug.Log($"Screen {screenType} is already active");
                return false;
            }

            return true;
        }

        public async UniTask RestartGame() {
            var screensToHide = _activeScreens.Keys.ToList();

            foreach (var iScreenType in screensToHide) {
                await HideScreenAsync(iScreenType, false);
            }

            _screenSwitcher.ClearHistory();
        }

        private void InitializeConfigs() {

            foreach (var config in _screenConfigs.Configs) {
                _configs[config.ScreenType] = config;
            }

            if (_layerContainers == null || _layerContainers.Length == 0)
                CreateLayerContainers();

            _isInitialized = true;
        }

        private void CreateLayerContainers() {
            var layerCount = Enum.GetValues(typeof(UILayer)).Length;
            _layerContainers = new Transform[layerCount];

            foreach (UILayer layer in Enum.GetValues(typeof(UILayer))) {
                var layerObject = new GameObject($"Layer_{layer}");
                layerObject.transform.SetParent(transform);

                var canvas = layerObject.AddComponent<Canvas>();
                canvas.sortingOrder = (int)layer;
                canvas.overrideSorting = true;

                _layerContainers[(int)layer / 100] = layerObject.transform;
            }
        }

        private async UniTask HideScreensOnLayer(UILayer layer, bool animated) {
            var screensToHide = _activeScreens
                .Where(kvp => _configs[kvp.Key].Layer == layer)
                .ToList();

            var hideTasks = screensToHide.Select(kvp => HideScreenAsync(kvp.Key, animated));
            await UniTask.WhenAll(hideTasks);
        }
    }
}
