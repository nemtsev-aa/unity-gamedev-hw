using R3;

namespace ShopSystem.Storages {
    
    public sealed class MoneyStorage {
        private readonly ReactiveProperty<long> _money;

        public ReadOnlyReactiveProperty<long> Money => _money;

        public MoneyStorage(long money) {
            _money = new ReactiveProperty<long>(money);
        }

        public void AddMoney(long money) =>
            _money.Value += money;

        public void SpendMoney(long money) =>
            _money.Value -= money;

        public void SetMoney(long money) =>
            _money.Value = money;
    }
}
