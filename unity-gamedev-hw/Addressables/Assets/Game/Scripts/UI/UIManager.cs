using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using UnityEngine;
using AssetManagementSystem;
using Cysharp.Threading.Tasks;
using UI.Components.Screens;
using UI.Services;
using UI.Components;

namespace UI.Core {

    public sealed class UIManager : MonoBehaviour {
        public ReadOnlyReactiveProperty<UIScreenType> ScreenOpened => _screenOpened;
        public ReadOnlyReactiveProperty<UIScreenType> ScreenClosed => _screenClosed;
        //public ReadOnlyReactiveProperty<UIScreenType> ScreenShowed => _screenShowed;

        [SerializeField] private Transform[] _layerContainers;

        private readonly Dictionary<UIScreenType, UIScreenConfig> _configs = new();
        private readonly Dictionary<UIScreenType, UIComponent> _activeScreens = new();

        private ReactiveProperty<UIScreenType> _screenOpened = new();
        private ReactiveProperty<UIScreenType> _screenClosed = new();
        //private ReactiveProperty<UIScreenType> _screenShowed = new();

        private ScreenFactory _screenFactory;
        private ScreenSwitcher _screenSwitcher;
        private UIScreenConfigs _screenConfigs;
        private bool _isInitialized;

        [Inject]
        public void Construct(UIScreenConfigs screenConfigs, AssetManager assetManager) {
            _screenConfigs = screenConfigs;
            _screenFactory = new ScreenFactory(assetManager);
            _screenSwitcher = new ScreenSwitcher(this);

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
            var screen = await _screenFactory.CreateScreenAsync<T>(screenType, config, parent);

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
                } else {
                    _screenFactory.ReturnToPool(screenType, screen);
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

    //public class UIManager : MonoBehaviour {


    //    [SerializeField] private Transform[] _layerContainers;

    //    private readonly Dictionary<UIScreenType, UIScreenConfig> _configs = new();
    //    private readonly Dictionary<UIScreenType, UIComponent> _activeScreens = new();
    //    private readonly Dictionary<UIScreenType, Queue<UIComponent>> _pooledScreens = new();

    //    private readonly Stack<UIScreenType> _screenHistory = new();

    //    private AssetManager _resourceManager;
    //    private UIScreenConfigs _screenConfigs;
    //    private bool _isInitialized;

    //    public ReadOnlyReactiveProperty<UIScreenType> ScreenOpened => _screenOpened;
    //    public ReadOnlyReactiveProperty<UIScreenType> ScreenClosed => _screenClosed;

    //    private ReactiveProperty<UIScreenType> _screenOpened = new();
    //    private ReactiveProperty<UIScreenType> _screenClosed = new();

    //    [Inject]
    //    public void Construct(UIScreenConfigs screenConfigs, AssetManager resourceManager) {
    //        _screenConfigs = screenConfigs;
    //        _resourceManager = resourceManager;
    //    }

    //    private void Awake() {
    //        InitializeConfigs();
    //        DontDestroyOnLoad(gameObject);
    //    }

    //    public async UniTask<T> ShowScreenAsync<T>(UIScreenType screenType, bool addToHistory = true, bool animated = true) where T : UIComponent {

    //        if (_isInitialized == false) {
    //            Debug.LogWarning("UIManager not initialized yet");
    //            return null;
    //        }

    //        var screen = await GetOrCreateScreenAsync<T>(screenType);
    //        if (screen == null)
    //            return null;

    //        // Закрываем предыдущий экран того же слоя если нужно
    //        var config = _configs[screenType];
    //        if (config.AllowMultipleInstances == false) {
    //            await HideScreensOnLayer(config.Layer, animated);
    //        }

    //        await screen.ShowAsync(animated);

    //        if (addToHistory && _screenHistory.Contains(screenType) == false) {
    //            _screenHistory.Push(screenType);
    //        }

    //        _activeScreens[screenType] = screen;
    //        _screenOpened.Value = screenType;

    //        return screen;
    //    }

    //    public async UniTask HideScreenAsync(UIScreenType screenType, bool animated = true) {

    //        if (_activeScreens.TryGetValue(screenType, out var screen)) {
    //            await screen.HideAsync(animated);

    //            var config = _configs[screenType];
    //            if (config.DestroyOnHide) {
    //                Destroy(screen.gameObject);
    //            } else {
    //                ReturnToPool(screenType, screen);
    //            }

    //            _activeScreens.Remove(screenType);
    //            RemoveFromHistory(screenType);
    //            _screenClosed.Value = screenType;
    //        }
    //    }

    //    public async UniTask ShowPreviousScreenAsync(bool animated = true) {

    //        if (_screenHistory.Count > 1) {
    //            _screenHistory.Pop();
    //            var previousScreen = _screenHistory.Pop();
    //            await ShowScreenAsync<UIComponent>(previousScreen, false, animated);
    //        }
    //    }

    //    public T GetActiveScreen<T>(UIScreenType screenType) where T : UIComponent {
    //        return _activeScreens.TryGetValue(screenType, out var screen) ? screen as T : null;
    //    }

    //    public bool IsScreenActive(UIScreenType screenType) {
    //        return _activeScreens.ContainsKey(screenType);
    //    }

    //    private void InitializeConfigs() {
    //        foreach (var config in _screenConfigs.Configs) {
    //            _configs[config.ScreenType] = config;
    //        }

    //        if (_layerContainers == null || _layerContainers.Length == 0) 
    //            CreateLayerContainers();

    //        _isInitialized = true;
    //    }

    //    private void CreateLayerContainers() {
    //        var layerCount = Enum.GetValues(typeof(UILayer)).Length;
    //        _layerContainers = new Transform[layerCount];

    //        foreach (UILayer layer in Enum.GetValues(typeof(UILayer))) {
    //            var layerObject = new GameObject($"Layer_{layer}");
    //            layerObject.transform.SetParent(transform);

    //            var canvas = layerObject.AddComponent<Canvas>();
    //            canvas.sortingOrder = (int)layer;
    //            canvas.overrideSorting = true;

    //            _layerContainers[(int)layer / 100] = layerObject.transform;
    //        }
    //    }

    //    private async UniTask<T> GetOrCreateScreenAsync<T>(UIScreenType screenType) where T : UIComponent {

    //        if (_pooledScreens.TryGetValue(screenType, out var pool) && pool.Count > 0) 
    //            return pool.Dequeue() as T;

    //        var config = _configs[screenType];
    //        var layerIndex = (int)config.Layer / 100;
    //        var parent = _layerContainers[layerIndex];

    //        var prefab = await _resourceManager.LoadAssetAsync<GameObject>(config.PrefabPath);
    //        var instance = Instantiate(prefab, parent);

    //        var component = instance.GetComponent<T>();
    //        if (component == null) {
    //            Debug.LogError($"Component {typeof(T)} not found on {screenType} prefab");
    //            Destroy(instance);
    //            return null;
    //        }

    //        component.Initialize();
    //        return component;
    //    }

    //    private void ReturnToPool(UIScreenType screenType, UIComponent screen) {
    //        if (_pooledScreens.ContainsKey(screenType) == false) 
    //            _pooledScreens[screenType] = new Queue<UIComponent>();

    //        _pooledScreens[screenType].Enqueue(screen);
    //    }

    //    private async UniTask HideScreensOnLayer(UILayer layer, bool animated) {
    //        var screensToHide = _activeScreens
    //            .Where(kvp => _configs[kvp.Key].Layer == layer)
    //            .ToList();

    //        var hideTasks = screensToHide.Select(kvp => HideScreenAsync(kvp.Key, animated));
    //        await UniTask.WhenAll(hideTasks);
    //    }

    //    private void RemoveFromHistory(UIScreenType screenType) {
    //        var tempStack = new Stack<UIScreenType>();

    //        while (_screenHistory.Count > 0) {
    //            var screen = _screenHistory.Pop();

    //            if (screen != screenType) 
    //                tempStack.Push(screen);
    //        }

    //        while (tempStack.Count > 0) {
    //            _screenHistory.Push(tempStack.Pop());
    //        }
    //    }
    //}
}
