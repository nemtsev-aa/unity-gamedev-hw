using StepByStepButler.Gameplay;
using StepByStepButler.Gameplay.Heroes;
using System.Collections.Generic;
using System.Linq;

namespace UI.Components.ActiveEffectViewSystem {

    public sealed class ActiveEffectViewPool {
        private readonly ActiveEffectViewConfigs _configs;
        private readonly ActiveEffectViewFactory _factory;
        private readonly HeroesProvider _heroes;
        private readonly UIService _uiService;
        private readonly Dictionary<int, ActiveEffectView> _activeViewsByHero = new();
        private List<ActiveEffectView> _views = new();

        private HeroListView RedPlayer => _uiService.GetRedPlayer();
        private HeroListView BluePlayer => _uiService.GetBluePlayer();

        public ActiveEffectViewPool(ActiveEffectViewConfigs configs,
                                    ActiveEffectViewFactory factory,
                                    HeroesProvider heroes,
                                    UIService uiService) {

            _configs = configs;
            _factory = factory;
            _heroes = heroes;
            _uiService = uiService;
        }

        public void CreatePool() {

            for (int i = 0; i < _configs.Configs.Count; i++) {
                var iConfig = _configs.Configs[i];

                if (_factory.TryGet(iConfig.Type, out var view) == true) {
                    view.Init(iConfig);

                    if (_views.Contains(view) == false) {
                        _views.Add(view);
                    }
                }
            }
        }

        public void ShowEffectByType(ActiveEffectType type, Entity entity, bool status) {
            int heroId = entity.Id;

            if (TryGetHeroView(heroId, out var heroView) == true) {
                var effectView = _views.FirstOrDefault(v => v.Type == type);

                if (effectView != null)
                    effectView.Activate(status, heroView.transform);
            }
        }

        public void Reset() {

            if (_views.Count == 0)
                return;

            for (int i = 0; i < _views.Count; i++) {
                var iView = _views[i];
                iView.Reset();
            }

            _views.Clear();
        }

        private bool TryGetHeroView(int heroId, out HeroView heroView) {
            heroView = null;

            if (_heroes.TryGetEntityById(heroId, out Entity entity) &&
                TryGetHeroViewByEntity(entity, out heroView) == true) {

                return true;
            }

            return false;
        }

        private bool TryGetHeroViewByEntity(Entity entity, out HeroView heroView) {
            var playerType = entity.GetComponent<PlayerComponent>().PlayerType;
            var viewIndex = entity.GetComponent<ViewIndexComponent>().Index;

            var listView = playerType == PlayerType.Red ? RedPlayer : BluePlayer;
            HeroView findedView = listView.GetView(viewIndex);

            if (findedView != null) {
                heroView = findedView;
                return true;
            }

            heroView = default;
            return false;
        }
    }
}
