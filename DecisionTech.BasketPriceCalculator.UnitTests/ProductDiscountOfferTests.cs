using System.Collections.Generic;
using DecisionTech.BasketPriceCalculator.Offers;
using DecisionTech.BasketPriceCalculator.Price;
using NUnit.Framework;

namespace DecisionTech.BasketPriceCalculator.UnitTests
{
    [TestFixture]
    public class ProductDiscountOfferTests
    {
        [SetUp]
        public void Setup()
        {
            _sut = new ProductDiscountOffer(new ProductPriceProvider(),
                new Product(ProductName.Milk),3);
        }

        //System Under Test
        private ProductDiscountOffer _sut;

        [Test]
        public void WhenThereIsNoProductInTheBasketThenThereIsNoDiscount()
        {
            //Arrange 
            var basketItems = new List<BasketItem>();

            //Act & Assert
            Assert.AreEqual(0, _sut.GetDiscount(basketItems));
        }

        [Test]
        public void WhenThereIsNoMilkInTheBasketThenThereIsNoDiscount()
        {
            //Arrange 
            var basketItems = new List<BasketItem>
            {
                new BasketItem(new Product(ProductName.Butter), 1),
                new BasketItem(new Product(ProductName.Bread), 1)
            };

            //Act & Assert
            Assert.AreEqual(0, _sut.GetDiscount(basketItems));
        }

        [Test]
        public void WhenThereIsNoEnoughMilkInTheBasketThenThereIsNoDiscount()
        {
            //Arrange 
            var basketItems = new List<BasketItem>
            {
                new BasketItem(new Product(ProductName.Milk), 3)
            };

            //Act & Assert
            Assert.AreEqual(0, _sut.GetDiscount(basketItems));
        }

        [TestCase(2, 0)]
        [TestCase(4, 1.15)]
        [TestCase(6, 1.15)]
        [TestCase(7, 1.15)]
        [TestCase(8, 2.30)]
        [TestCase(10, 2.30)]
        [TestCase(12, 3.45)]
        [TestCase(13, 3.45)]
        public void WhenThereIsXMilkInTheBasketThenThereIsYDiscount(int milkQuantity, decimal discountExpected)
        {
            //Arrange 
            var basketItems = new List<BasketItem>
            {
                new BasketItem(new Product(ProductName.Milk), milkQuantity)
            };

            //Act & Assert
            Assert.AreEqual(discountExpected, _sut.GetDiscount(basketItems));
        }
    }
}