using System.Collections.Generic;
using DecisionTech.BasketPriceCalculator.Offers;
using DecisionTech.BasketPriceCalculator.Price;
using NUnit.Framework;

namespace DecisionTech.BasketPriceCalculator.UnitTests
{
    public class BuyTwoButterAndGetOneBreadHalfPricedOfferTests
    {
        // System Under Test
        private BuyTwoButterAndGetOneBreadHalfPricedOffer _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new BuyTwoButterAndGetOneBreadHalfPricedOffer(new ProductPriceProvider());
        }

        [Test]
        public void WhenThereIsNoProductInTheBasketThenNoDiscount()
        {
            // Arrange
            var basketItems = new List<BasketItem>();

            // Act & Assert
            Assert.AreEqual(0, _sut.GetDiscount(basketItems));
        }

        [Test]
        public void WhenThereIsNoBreadInTheBasketThenThereIsNoDiscount()
        {
            // Arrange
            var basketItems = new List<BasketItem>
            {
                new BasketItem(new Product(ProductName.Butter), 2),
                new BasketItem(new Product(ProductName.Milk), 1)
            };

            // Act & Assert
            Assert.AreEqual(0, _sut.GetDiscount(basketItems));
        }

        [Test]
        public void WhenThereIsNoButterInTheBasketThenThereIsNoDiscount()
        {
            // Arrange
            var basketItems = new List<BasketItem>
            {
                new BasketItem(new Product(ProductName.Bread), 1),
                new BasketItem(new Product(ProductName.Milk), 1)
            };

            // Act & Assert
            Assert.AreEqual(0, _sut.GetDiscount(basketItems));
        }

        [TestCase(2, 0, 0.0)]
        [TestCase(2, 1, 0.5)]
        [TestCase(2, 2, 0.5)]
        [TestCase(2, 3, 0.5)]
        [TestCase(3, 0, 0.0)]
        [TestCase(3, 1, 0.5)]
        [TestCase(3, 2, 0.5)]
        [TestCase(4, 0, 0.0)]
        [TestCase(4, 1, 0.5)]
        [TestCase(4, 2, 1.0)]
        [TestCase(4, 3, 1.0)]
        [TestCase(4, 4, 1.0)]
        [TestCase(4, 5, 1.0)]
        [TestCase(10, 0, 0.0)]
        [TestCase(10, 5, 2.5)]
        [TestCase(10, 10, 2.5)]
        public void WhenThereAreXButterAndYBreadInTheBasketThenThereIsZDiscount(int butterQuantity, int breadQuantity,
            decimal discountExpected)
        {
            // Arrange
            var basketItems = new List<BasketItem>
            {
                new BasketItem(new Product(ProductName.Butter), butterQuantity)
            };
            if (breadQuantity > 0)
                basketItems.Add(new BasketItem(new Product(ProductName.Bread), breadQuantity));

            // Act & Assert
            Assert.AreEqual(discountExpected, _sut.GetDiscount(basketItems));
        }
    }
}