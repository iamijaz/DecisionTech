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

            if (basketItems.Any(item =>
                _offerProducts.Select(p => p.ProductName).Contains(item.Product.ProductName) && item.Quantity == 0)) return 0;

            
            var totalProduct1 = basketItems.Where(item => item.Product.ProductName == offerProduct1.ProductName)
                .Sum(item => item.Quantity);

            
            var totalProduct2 = basketItems.Where(item => item.Product.ProductName == offerProduct2.ProductName)
                .Sum(item => item.Quantity);

            var product1Pairs = totalProduct1 / 2;

            var product2Price = _productPriceProvider.GetPrice(offerProduct2.ProductName);
            var product2Discount = product2Price / 100 * _discountPercentage;

            if (product1Pairs >= totalProduct2)
                return totalProduct2 * product2Discount;

            return product1Pairs * product2Discount;
        }
    }
}