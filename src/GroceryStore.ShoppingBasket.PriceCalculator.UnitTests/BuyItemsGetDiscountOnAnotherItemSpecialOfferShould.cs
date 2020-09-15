using System;
using FluentAssertions;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.UnitTests
{
    public class BuyItemsGetDiscountOnAnotherItemSpecialOfferShould
    {
        [Fact]
        public void ApplyDiscountWhenIBuyTwoEligibleItemsAndOneTargetItem()
        {
            var basket = GivenABeansAndBreadBasket();
            var class_under_test = GivenABuyTwoBeansGetHalfPriceBreadOffer();
            class_under_test.AppliesTo(basket).Should().BeTrue();
            var result = class_under_test.ApplyOfferTo(basket);
            result.OfferDescription.Should().Be("Buy 2 cans of beans and get a loaf of bread for half price");
            result.TotalDiscountGbp.Should().Be(1m);
        }
        
        [Fact]
        public void Apply25PercentOffDiscountWhenIBuyTwoEligibleItemsAndOneTargetItem()
        {
            var basket = GivenABeansAndBreadBasket();
            var class_under_test = new BuyItemsGetDiscountOnAnotherItemSpecialOffer("beans", 2, "bread", 0.25m);
            class_under_test.AppliesTo(basket).Should().BeTrue();
            var result = class_under_test.ApplyOfferTo(basket);
            result.OfferDescription.Should().Be("Buy 2 cans of beans and get a loaf of bread with 25% off");
            result.TotalDiscountGbp.Should().Be(0.5m);
        }
        
        [Fact]
        public void ApplyDiscountTwiceWhenIBuyFourEligibleItemsAndTwoTargetItems()
        {
            var basket = new Basket()
            {
                CheckoutDateUtc = DateTime.UtcNow,
                Goods = new[]
                {
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Bread", UnitOfMeasurement = "loaf", GbpPrice = 2m},
                    new Good() {Name = "Bread", UnitOfMeasurement = "loaf", GbpPrice = 2m}
                }
            };
            var class_under_test = GivenABuyTwoBeansGetHalfPriceBreadOffer();
            class_under_test.AppliesTo(basket).Should().BeTrue();
            var result = class_under_test.ApplyOfferTo(basket);
            result.OfferDescription.Should().Be("Buy 2 cans of beans and get a loaf of bread for half price");
            result.TotalDiscountGbp.Should().Be(2m);
        }
        
        [Fact]
        public void ApplyDiscountOnceWhenIBuyFourEligibleItemsAndOneTargetItem()
        {
            var basket = new Basket()
            {
                CheckoutDateUtc = DateTime.UtcNow,
                Goods = new[]
                {
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Bread", UnitOfMeasurement = "loaf", GbpPrice = 2m}
                }
            };
            var class_under_test = GivenABuyTwoBeansGetHalfPriceBreadOffer();
            class_under_test.AppliesTo(basket).Should().BeTrue();
            var result = class_under_test.ApplyOfferTo(basket);
            result.OfferDescription.Should().Be("Buy 2 cans of beans and get a loaf of bread for half price");
            result.TotalDiscountGbp.Should().Be(1m);
        }
        
        [Fact]
        public void NotApplyWhenBasketIsMissingTargetItem()
        {
            var basket = new Basket()
            {
                CheckoutDateUtc = DateTime.UtcNow,
                Goods = new[]
                {
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m}
                }
            };
            var class_under_test = GivenABuyTwoBeansGetHalfPriceBreadOffer();
            class_under_test.AppliesTo(basket).Should().BeFalse();
        }
        
        [Fact]
        public void ThrowInvalidOperationExceptionWhenAttemptingToApplyIneligibleOffer()
        {
            var basket = new Basket()
            {
                CheckoutDateUtc = DateTime.UtcNow,
                Goods = new Good[] { }
            };
            var class_under_test = GivenABuyTwoBeansGetHalfPriceBreadOffer();
            class_under_test.AppliesTo(basket).Should().BeFalse();
            Assert.Throws<InvalidOperationException>(() => class_under_test.ApplyOfferTo(basket));
        }

        private static BuyItemsGetDiscountOnAnotherItemSpecialOffer GivenABuyTwoBeansGetHalfPriceBreadOffer()
        {
            return new BuyItemsGetDiscountOnAnotherItemSpecialOffer("beans", 2, "bread", 0.5m);
        }

        private static Basket GivenABeansAndBreadBasket()
        {
            return new Basket()
            {
                CheckoutDateUtc = DateTime.UtcNow,
                Goods = new[]
                {
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Beans", UnitOfMeasurement = "can", GbpPrice = 1.5m},
                    new Good() {Name = "Bread", UnitOfMeasurement = "loaf", GbpPrice = 2m}
                }
            };
        }
    }
}