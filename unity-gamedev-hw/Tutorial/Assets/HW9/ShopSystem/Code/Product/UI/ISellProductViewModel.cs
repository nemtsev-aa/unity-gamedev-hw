using R3;

namespace ShopSystem.Product.UI {

    public interface ISellProductViewModel : IProductViewModel {
        Observable<bool> CanSell { get; }
        ReactiveCommand SellCommand { get; }
        void Sell(int ammount = 1);
    }
}
