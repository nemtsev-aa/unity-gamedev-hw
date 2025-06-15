using ShopSystem.UI;
using System;
using UnityEngine;

namespace ShopSystem.Product.UI {

    public interface IProductViewModel : IViewModel, IDisposable {
        string Title { get; }
        string Description { get; }
        Sprite Icon { get; }
        string Price { get; }
    }
}
