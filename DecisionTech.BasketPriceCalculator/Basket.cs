using System.Collections.Generic;
using System.Linq;
using DecisionTech.BasketPriceCalculator.Discount;
using DecisionTech.BasketPriceCalculator.Price;

namespace DecisionTech.BasketPriceCalculator
{
    public class Basket
    {
        private readonly IDiscountCalculator _discountCalculator;
        private readonly IProductPriceProvider _productPriceProvider;

        public Basket(IProductPriceProvider productPriceProvider, IDiscountCalculator discountCalculator)
        {
            BasketItems = new List<BasketItem>();
            _productPriceProvider = productPriceProvider;
            _discountCalculator = discountCalculator;
        }

        public List<BasketItem> BasketItems { get; set; }

        public decimal GetGrandTotal()
        {
            return GetRunningTotal() - _discountCalculator.CalculateDiscount(BasketItems);
        }

        private decimal GetRunningTotal()
        {
            return BasketItems.Sum(item => _productPriceProvider.GetPrice(item.Product.ProductName) * item.Quantity);
        }
    }
}