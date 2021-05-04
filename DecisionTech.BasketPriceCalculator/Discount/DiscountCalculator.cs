using System.Collections.Generic;
using System.Linq;
using DecisionTech.BasketPriceCalculator.Offers;

namespace DecisionTech.BasketPriceCalculator.Discount
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly List<IOffer> _offers;

        public DiscountCalculator(List<IOffer> offers)
        {
            _offers = offers;
        }

        public decimal CalculateDiscount(List<BasketItem> basketItems)
        {
            return _offers.Sum(o => o.GetDiscount(basketItems));
        }
    }
}