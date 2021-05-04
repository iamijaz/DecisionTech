using System.Collections.Generic;

namespace DecisionTech.BasketPriceCalculator.Price
{
    public class ProductPriceProvider : IProductPriceProvider
    {
        private readonly Dictionary<ProductName, decimal> _productPrices = new Dictionary<ProductName, decimal>
        {
            {ProductName.Butter, 0.80M},
            {ProductName.Milk, 1.15M},
            {ProductName.Bread, 1.00M}
        };

        public decimal GetPrice(ProductName productName)
        {
            return _productPrices[productName];
        }
    }
}