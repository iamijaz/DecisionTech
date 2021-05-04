namespace DecisionTech.BasketPriceCalculator.Price
{
    public interface IProductPriceProvider
    {
        decimal GetPrice(ProductName productName);
    }
}