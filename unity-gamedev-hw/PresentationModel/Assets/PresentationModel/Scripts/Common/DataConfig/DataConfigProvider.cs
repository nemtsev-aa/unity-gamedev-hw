using System;
using System.Collections.Generic;

namespace PresentationModel {
    public class DataConfigProvider {

        private readonly IReadOnlyList<DataConfig> _configs;

        public DataConfigProvider(DataConfigs configs) {
            _configs = configs.Configs;
        }

        public bool TryGetConfig<T>(out T data) where T : DataConfig {
            Type targetType = typeof(T);
            
            for (int i = 0; i < _configs.Count; i++) {
                var config = _configs[i];
                
                if (config.GetType() == targetType) {
                    data = (T)config;
                    return true;
                }
            }

            data = default;
            return false;
        }
    }
}


