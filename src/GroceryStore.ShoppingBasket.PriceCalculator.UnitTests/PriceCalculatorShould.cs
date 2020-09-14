using System;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib;
using Moq;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.UnitTests
{
    public class PriceCalculatorShould
    {
        [Fact]
        public void ThrowInvalidOperationExceptionWhenBasketIsEmpty()
        {
            var class_under_test =
                new Lib.PriceCalculator(Mock.Of<IGoodRepository>(), Mock.Of<ISpecialOfferRepository>());
            
            Assert.Throws<InvalidOperationException>(() => class_under_test.PriceBasket(Array.Empty<string>()));
        }
    }
}