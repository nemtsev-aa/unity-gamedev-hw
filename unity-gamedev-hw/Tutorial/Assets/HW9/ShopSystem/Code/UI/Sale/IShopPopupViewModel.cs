using ObservableCollections;
using ShopSystem.Product.UI;

namespace ShopSystem.UI {

    public interface IShopPopupViewModel : IViewModel {
        IReadOnlyObservableList<ISaleProductViewModel> ProductPresenters { get; }
    }
}
