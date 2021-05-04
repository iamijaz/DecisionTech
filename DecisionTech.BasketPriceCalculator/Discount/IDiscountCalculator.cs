using System.Collections.Generic;

namespace DecisionTech.BasketPriceCalculator.Discount
{
    public interface IDiscountCalculator
    {
        decimal CalculateDiscount(List<BasketItem> basketItems);
    }
}