using System.Collections.Generic;
using DecisionTech.BasketPriceCalculator.Discount;
using DecisionTech.BasketPriceCalculator.Offers;
using DecisionTech.BasketPriceCalculator.Price;
using NUnit.Framework;

namespace DecisionTech.BasketPriceCalculator.Specs
{
    [TestFixture]
    public class BasketSpecs
    {
        [SetUp]
        public void Setup()
        {
            _sut = new Basket(new ProductPriceProvider(),
                new DiscountCalculator(new List<IOffer>
                    {
                        new PercentageDiscountOffer(
                            new ProductPriceProvider(),
                            new List<Product>
                            {
                                new Product(ProductName.Butter),
                                new Product(ProductName.Bread)
                            },
                            50),
                        new ProductDiscountOffer(
                            new ProductPriceProvider(),
                            new Product(ProductName.Milk),
                            3)
                    }
                ));
        }


        // System Under Test
        private Basket _sut;

        [Test]
        public void
            Given_the_basket_has_1_bread_1_butter_and_1_milk_When_I_total_the_basket_Then_the_total_should_be_GBP_2and95()
        {
            // Given
            _sut.BasketItems.Add(new BasketItem(new Product(ProductName.Bread), 1));
            _sut.BasketItems.Add(new BasketItem(new Product(ProductName.Butter), 1));
            _sut.BasketItems.Add(new BasketItem(new Product(ProductName.Milk), 1));

            // When
            var grandTotal = _sut.GetGrandTotal();

            // Then
            Assert.AreEqual(2.95M, grandTotal);
        }

        [Test]
        public void
            Given_the_basket_has_2_butter_and_2_bread_When_I_total_the_basket_Then_the_total_should_be_GBP_3and10()
        {
            // Given
            _sut.BasketItems.Add(new BasketItem(new Product(ProductName.Bread), 2));
            _sut.BasketItems.Add(new BasketItem(new Product(ProductName.Butter), 2));

            // When
            var grandTotal = _sut.GetGrandTotal();

            // Then
            Assert.AreEqual(3.10M, grandTotal);
        }

        [Test]
        public void Given_the_basket_has_4_milk_When_I_total_the_basket_Then_the_total_should_be_GBP_3and45()
        {
            // Given
            _sut.BasketItems.Add(new BasketItem(new Product(ProductName.Milk), 4));

            // When
            var grandTotal = _sut.GetGrandTotal();

            // Then
            Assert.AreEqual(3.45M, grandTotal);
        }

        [Test]
        public void
            Given_the_basket_has_2_butter_1_bread_and_8_milk_When_I_total_the_basket_Then_the_total_should_be_GBP_9and00()
        {
            // Given
            _sut.BasketItems.Add(new BasketItem(new Product(ProductName.Butter), 2));
            _sut.BasketItems.Add(new BasketItem(new Product(ProductName.Bread), 1));
            _sut.BasketItems.Add(new BasketItem(new Product(ProductName.Milk), 8));

            // When
            var grandTotal = _sut.GetGrandTotal();

            // Then
            Assert.AreEqual(9.00M, grandTotal);
        }
    }
}