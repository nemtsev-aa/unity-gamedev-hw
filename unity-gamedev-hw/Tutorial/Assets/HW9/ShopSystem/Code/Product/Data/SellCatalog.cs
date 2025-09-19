using System;
using System.Collections.Generic;

namespace ShopSystem.Product.Data {

    /// <summary>
    /// Каталог товаров для продажи с базовой логикой добавления/удаления и проверки наличия.
    /// </summary>
    public sealed class SellCatalog {
        private readonly Dictionary<string, int> _products = new Dictionary<string, int>();

        public IReadOnlyDictionary<string, int> Products => _products;

        /// <summary>
        /// Добавляет товар в каталог или увеличивает его количество, если он уже есть.
        /// </summary>
        /// <param name="productId">ID товара.</param>
        /// <param name="amount">Количество (по умолчанию 1).</param>
        public void AddProduct(string productId, int amount = 1) {
            if (string.IsNullOrEmpty(productId))
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive.");

            if (_products.TryGetValue(productId, out int currentAmount)) {
                _products[productId] = currentAmount + amount;
                return;
            }

            _products.Add(productId, amount);
        }

        /// <summary>
        /// Удаляет товар из каталога или уменьшает его количество.
        /// </summary>
        /// <param name="productId">ID товара.</param>
        /// <param name="amount">Количество (по умолчанию 1).</param>
        /// <returns>True, если товар успешно удалён/уменьшен, иначе false.</returns>
        public bool TryRemoveProduct(string productId, int amount = 1) {
            if (string.IsNullOrEmpty(productId))
                throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive.");

            if (_products.TryGetValue(productId, out int currentAmount) == false)
                return false;

            _products[productId] = currentAmount - amount;

            if (_products[productId] > 0) {
                //Debug.Log($"SellCatalog: {productId} {_products[productId]}");
                return true;
            }

            _products.Remove(productId);
            return true;
        }

        /// <summary>
        /// Полностью удаляет товар из каталога, независимо от количества.
        /// </summary>
        public bool RemoveProductCompletely(string productId) {
            return _products.Remove(productId);
        }

        /// <summary>
        /// Проверяет, есть ли товар в каталоге.
        /// </summary>
        public bool ContainsProduct(string productId) {
            return _products.ContainsKey(productId);
        }

        /// <summary>
        /// Возвращает количество товара в каталоге. Если товара нет, вернёт 0.
        /// </summary>
        public int GetProductAmount(string productId) {
            return _products.TryGetValue(productId, out int amount) ? amount : 0;
        }

        /// <summary>
        /// Возвращает все товары в каталоге в формате ID → количество.
        /// </summary>
        public IReadOnlyDictionary<string, int> GetAllProducts() {
            return _products;
        }

        /// <summary>
        /// Очищает каталог полностью.
        /// </summary>
        public void Clear() {
            _products.Clear();
        }
    }
}
