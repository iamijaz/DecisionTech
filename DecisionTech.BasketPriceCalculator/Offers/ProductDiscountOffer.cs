using System.Collections.Generic;
using System.Linq;
using DecisionTech.BasketPriceCalculator.Price;

namespace DecisionTech.BasketPriceCalculator.Offers
{
    public class ProductDiscountOffer : IOffer
    {
        private readonly IProductPriceProvider _productPriceProvider;
        private readonly Product _offerProduct;
        private readonly int _minimumEligibleProducts;


        public ProductDiscountOffer(IProductPriceProvider productPriceProvider, Product offerProduct, int minimumEligibleProducts)
        {
            _productPriceProvider = productPriceProvider;
            _offerProduct = offerProduct;
            _minimumEligibleProducts = minimumEligibleProducts;
        }

        public decimal GetDiscount(List<BasketItem> basketItems)
        {
            return IsValidQuantity(basketItems) ? CalculateDiscount(basketItems) : 0.0M;
        }

        private bool IsValidQuantity(IEnumerable<BasketItem> basketItems)
        {
            return TotalProducts(basketItems) > _minimumEligibleProducts;
        }

        private decimal CalculateDiscount(IEnumerable<BasketItem> basketItems)
        {
            var discountSeed = TotalProducts(basketItems) / (_minimumEligibleProducts + 1);
            return _productPriceProvider.GetPrice(_offerProduct.ProductName) * discountSeed;
        }

        private  int TotalProducts(IEnumerable<BasketItem> basketItems)
        {
            var totalProducts = basketItems.Where(item => item.Product.ProductName == _offerProduct.ProductName)
                .Sum(item => item.Quantity);
            return totalProducts;
        }
    }
}