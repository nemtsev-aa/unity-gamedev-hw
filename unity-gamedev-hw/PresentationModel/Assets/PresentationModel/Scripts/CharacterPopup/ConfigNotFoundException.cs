using System;

namespace PresentationModel {
    public class ConfigNotFoundException : Exception {
        public ConfigNotFoundException(Type configType)
            : base($"Configuration not found for type: {configType.Name}") { }
    }
}


