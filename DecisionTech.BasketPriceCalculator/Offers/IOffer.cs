using System.Collections.Generic;

namespace DecisionTech.BasketPriceCalculator.Offers
{
    public interface IOffer
    {
        decimal GetDiscount(List<BasketItem> basketItems);
    }
}