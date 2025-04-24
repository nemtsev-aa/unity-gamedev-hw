using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine {
    //Нельзя менять!
    [Serializable]
    public sealed class ResourceService {
        [ShowInInspector, ReadOnly]
        private Dictionary<string, Resource> _sceneResources = new();

        public void SetResources(IEnumerable<Resource> resources) {
            _sceneResources = resources.ToDictionary(it => it.ID);
        }

        public IEnumerable<Resource> GetResources() {
            return _sceneResources.Values;
        }
    }
}