using System.Collections.Generic;
using System;
using Zenject;

namespace Pattern_Memento {

    public class MementoServicesFactory : IMementoServicesFactory {
        private readonly DiContainer _container;
        private readonly Dictionary<MementoTypes, IMementoHandler> _managers;
        private readonly Dictionary<MementoTypes, MomemtosPopup> _popups;

        public MementoServicesFactory(
            DiContainer container,
            [Inject(Id = MementoTypes.Units)] IMementoHandler unitsManager,
            [Inject(Id = MementoTypes.Resources)] IMementoHandler resourcesManager,
            [Inject(Id = MementoTypes.Units)] MomemtosPopup unitsPopup,
            [Inject(Id = MementoTypes.Resources)] MomemtosPopup resourcesPopup) {
            
            _container = container;
            
            _managers = new Dictionary<MementoTypes, IMementoHandler> {
                { MementoTypes.Units, unitsManager },
                { MementoTypes.Resources, resourcesManager }
            };

            _popups = new Dictionary<MementoTypes, MomemtosPopup> {
                { MementoTypes.Units, unitsPopup },
                { MementoTypes.Resources, resourcesPopup }
            };
        }

        public MementoServices Create(MementoTypes type) {
            if (_managers.TryGetValue(type, out var manager) == false)
                throw new ArgumentException($"No manager registered for type {type}");

            if (_popups.TryGetValue(type, out var popup) == false)
                throw new ArgumentException($"No popup registered for type {type}");

            var history = new MementoHistory();
            var controller = new MementoHistoryController(history, popup, manager);

            return new MementoServices(type, history, controller, popup, manager);
        }
    }
}

