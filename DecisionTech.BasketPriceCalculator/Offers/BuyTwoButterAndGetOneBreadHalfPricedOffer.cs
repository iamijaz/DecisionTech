using System.Collections.Generic;
using System.Linq;
using DecisionTech.BasketPriceCalculator.Price;

namespace DecisionTech.BasketPriceCalculator.Offers
{
    public class BuyTwoButterAndGetOneBreadHalfPricedOffer : IOffer
    {
        private const int DiscountPercentage = 50;
        private readonly IProductPriceProvider _productPriceProvider;

        public BuyTwoButterAndGetOneBreadHalfPricedOffer(IProductPriceProvider productPriceProvider)
        {
            _productPriceProvider = productPriceProvider;
        }

        public decimal GetDiscount(List<BasketItem> basketItems)
        {
            var eligibleProducts = new[] {ProductName.Butter, ProductName.Bread};
            if (basketItems.Any(item =>
                eligibleProducts.Contains(item.Product.ProductName) && item.Quantity == 0)) return 0;

            var totalButters = basketItems.Where(item => item.Product.ProductName == ProductName.Butter)
                .Sum(item => item.Quantity);

            var totalBreads = basketItems.Where(item => item.Product.ProductName == ProductName.Bread)
                .Sum(item => item.Quantity);

            var butterPairs = totalButters / 2;

            var breadPrice = _productPriceProvider.GetPrice(ProductName.Bread);
            var breadDiscount = breadPrice / 100 * DiscountPercentage;

            if (butterPairs >= totalBreads)
                return totalBreads * breadDiscount;

            return butterPairs * breadDiscount;
        }
    }
}