using System;
using FluentAssertions;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.AcceptanceTests
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldPass()
        {
            2.Should().BeGreaterThan(1);
        }
    }
}