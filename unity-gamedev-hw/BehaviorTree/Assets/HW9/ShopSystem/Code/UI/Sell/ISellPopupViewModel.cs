using R3;
using ObservableCollections;
using ShopSystem.Product.UI;

namespace ShopSystem.UI {
    public interface ISellPopupViewModel : IViewModel {
        IObservableCollection<ISellProductViewModel> ProductPresenters { get; }
        Observable<int> SellProductCount { get; }
    }
}
