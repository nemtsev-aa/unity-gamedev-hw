using System;

namespace Client.Components.Health {

    [Serializable]
    public struct Health {
        public int Value;
        public int MaxValue;
    }
}
