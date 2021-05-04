using System;

namespace DecisionTech.BasketPriceCalculator
{
    public class BasketItem
    {
        public BasketItem(Product product, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException($"{product.ProductName} quantity should be greater than 0");
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }
        public int Quantity { get; }
    }
}