using System;

namespace Client.Components.Attack {

    [Serializable]
    public struct AttackerTag {
        public int SourceID;
        public float ScaningRange;
        public float AttackRange;
        public float AttackRate;
    }
}