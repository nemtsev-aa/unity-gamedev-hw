using UnityEngine;
using Client.Components.Common;

namespace Units.View {
    public sealed class UnitViewModel {

        public UnitViewModel(UnitViewConfig config) {
            Type = config.Type;
            Name = Type.ToString();
            Sprite = config.Sprite;
        }

        public UnitTypes Type { get; private set; }
        public string Name { get; private set; }
        public Sprite Sprite { get; private set; }
    }
}