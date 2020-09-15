using System;
using FluentAssertions;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.UnitTests
{
    public class TimeLimitedPercentageDiscountOfferShould
    {
        [Fact]
        public void NotApplyWhenCurrentDateIsOutsideOfEligiblityPeriod()
        {
            var class_under_test = new TimeLimitedPercentageDiscountSpecialOffer("Ham",
                DateTime.UtcNow.AddMonths(-2), DateTime.UtcNow.AddMonths(-1), 0.2m);
            var basket = GivenAHamBasket();
            class_under_test.AppliesTo(basket).Should().BeFalse();
            var exception = Assert.Throws<InvalidOperationException>(() => class_under_test.ApplyOfferTo(basket));
            exception.Message.Should().Be("This basket is not eligible for this offer.");
        }
        
        [Fact]
        public void ApplyWhenCurrentDateIsInsideOfEligiblityPeriod()
        {
            var class_under_test = new TimeLimitedPercentageDiscountSpecialOffer("Ham",
                DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddMonths(1), 0.2m);
            var basket = GivenAHamBasket();
            class_under_test.AppliesTo(basket).Should().BeTrue();
            var result = class_under_test.ApplyOfferTo(basket);

            result.OfferDescription.Should().Be("Ham 20% off");
            result.TotalDiscountGbp.Should().Be(0.4m);
        }
                
        [Fact]
        public void NotApplyWhenNoEligibleGoodsArePresentInBasket()
        {
            var class_under_test = new TimeLimitedPercentageDiscountSpecialOffer("Spam",
                DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddMonths(1), 0.2m);
            var basket = GivenAHamBasket();
            class_under_test.AppliesTo(basket).Should().BeFalse();
            var exception = Assert.Throws<InvalidOperationException>(() => class_under_test.ApplyOfferTo(basket));
            exception.Message.Should().Be("This basket is not eligible for this offer.");
        }

        private static Basket GivenAHamBasket()
        {
            return new Basket(){CheckoutDateUtc = DateTime.UtcNow, Goods = new [] {new Good() {Name = "Ham", GbpPrice = 2m}}};
        }
    }
}