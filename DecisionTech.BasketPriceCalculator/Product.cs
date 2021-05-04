namespace DecisionTech.BasketPriceCalculator
{
    public class Product
    {
        public Product(ProductName productName)
        {
            ProductName = productName;
        }

        public ProductName ProductName { get; }
    }
}