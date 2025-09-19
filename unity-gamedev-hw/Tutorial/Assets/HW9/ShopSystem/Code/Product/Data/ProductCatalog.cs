using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem.Product.Data {

    [CreateAssetMenu(
        fileName = nameof(ProductCatalog),
        menuName = "Data/New " + nameof(ProductCatalog))
    ]
    public sealed class ProductCatalog : ScriptableObject {
        [field: SerializeField] public List<ProductInfo> Products { get; private set; }

        public ProductInfo GetProductInfo(string productId) {

            for (int i = 0; i < Products.Count; i++) {
                var iInfo = Products[i];

                if (iInfo.ID == productId)
                    return iInfo;
            }

            throw new ArgumentNullException($"Product [{productId}] not found in catalog");
        }

        public bool TryGetProductInfo(string productId, out ProductInfo productInfo) {

            for (int i = 0; i < Products.Count; i++) {
                var iInfo = Products[i];

                if (iInfo.ID == productId) {
                    productInfo = iInfo;
                    return true;
                }
            }

            productInfo = null;
            return false;
        }
    }
}
