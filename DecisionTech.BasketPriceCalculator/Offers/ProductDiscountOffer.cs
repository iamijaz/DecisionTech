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
            var totalProducts = basketItems.Where(item => item.Product.ProductName == _offerProduct.ProductName)
                .Sum(item => item.Quantity);
            if (totalProducts >= _minimumEligibleProducts)
            {
                var discountSeed = totalProducts / (_minimumEligibleProducts + 1);
                var productPrice = _productPriceProvider.GetPrice(_offerProduct.ProductName);
                return productPrice * discountSeed;
            }

            return 0;
        }
    }
}