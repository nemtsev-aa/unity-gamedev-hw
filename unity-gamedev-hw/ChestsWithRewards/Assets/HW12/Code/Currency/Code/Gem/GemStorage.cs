using System;

namespace ShopSystem.Storages {

    public sealed class GemStorage {
        public event Action<long> OnGemChanged;

        public long Gem { get; private set; }

        public GemStorage(long money) {
            Gem = money;
        }

        public void AddGem(long money) {
            Gem += money;
            OnGemChanged?.Invoke(Gem);
        }

        public void SpendGem(long money) {
            Gem -= money;
            OnGemChanged?.Invoke(Gem);
        }
    }
}
