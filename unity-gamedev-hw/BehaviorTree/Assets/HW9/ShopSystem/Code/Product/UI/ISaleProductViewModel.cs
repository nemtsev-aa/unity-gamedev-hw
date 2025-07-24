using R3;

namespace ShopSystem.Product.UI {

    public interface ISaleProductViewModel : IProductViewModel {
        Observable<bool> CanBuy { get; }
        ReactiveCommand BuyCommand { get; }
        void Buy();
    }
}
