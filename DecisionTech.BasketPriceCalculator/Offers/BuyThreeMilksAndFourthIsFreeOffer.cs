using System.Collections.Generic;
using System.Linq;
using DecisionTech.BasketPriceCalculator.Price;

namespace DecisionTech.BasketPriceCalculator.Offers
{
    public class BuyThreeMilksAndFourthIsFreeOffer : IOffer
    {
        private const int MinimumEligibleQuantity = 3;
        private readonly IProductPriceProvider _productPriceProvider;

        public BuyThreeMilksAndFourthIsFreeOffer(IProductPriceProvider productPriceProvider)
        {
            _productPriceProvider = productPriceProvider;
        }

        public decimal GetDiscount(List<BasketItem> basketItems)
        {
            var totalMilks = basketItems.Where(item => item.Product.ProductName == ProductName.Milk)
                .Sum(item => item.Quantity);
            if (totalMilks >= MinimumEligibleQuantity)
            {
                var discountSeed = totalMilks / (MinimumEligibleQuantity + 1);
                var milkPrice = _productPriceProvider.GetPrice(ProductName.Milk);
                return milkPrice * discountSeed;
            }

            return 0;
        }
    }
}