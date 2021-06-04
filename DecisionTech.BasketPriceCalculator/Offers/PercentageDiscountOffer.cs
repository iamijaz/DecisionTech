using System.Collections.Generic;
using System.Linq;
using DecisionTech.BasketPriceCalculator.Price;

namespace DecisionTech.BasketPriceCalculator.Offers
{
    public class PercentageDiscountOffer : IOffer
    {
        private readonly IProductPriceProvider _productPriceProvider;
        private readonly List<Product> _offerProducts;
        private readonly decimal _discountPercentage;

        public PercentageDiscountOffer(IProductPriceProvider productPriceProvider, List<Product> offerProducts, decimal discountPercentage)
        {
            _productPriceProvider = productPriceProvider;
            _offerProducts = offerProducts;
            _discountPercentage = discountPercentage;
        }

        public decimal GetDiscount(List<BasketItem> basketItems)
        {
            var offerProduct1 = _offerProducts.First();
            var offerProduct2 = _offerProducts.Skip(1).First();

            return IsValidQuantity(basketItems) ? CalculateDiscount(basketItems, offerProduct1, offerProduct2) : 0.0M;
        }

        private bool IsValidQuantity(IEnumerable<BasketItem> basketItems)
        {
            return !basketItems.Any(item =>
                _offerProducts.Select(p => p.ProductName).Contains(item.Product.ProductName) && item.Quantity == 0);
        }

        private decimal CalculateDiscount(IReadOnlyCollection<BasketItem> basketItems, Product offerProduct1, Product offerProduct2)
        {
            var totalProduct1 = TotalProducts(basketItems, offerProduct1);
            var product1Pairs = totalProduct1 / 2;

            var product2Price = _productPriceProvider.GetPrice(offerProduct2.ProductName);
            var product2Discount = product2Price / 100 * _discountPercentage;

            var totalProduct2 = TotalProducts(basketItems, offerProduct2);
            if (product1Pairs >= totalProduct2)
                return totalProduct2 * product2Discount;

            return product1Pairs * product2Discount;
        }

        private static int TotalProducts(IEnumerable<BasketItem> basketItems, Product product)
        {
            var totalProducts = basketItems.Where(item => item.Product.ProductName == product.ProductName)
                .Sum(item => item.Quantity);
            return totalProducts;
        }
    }
}