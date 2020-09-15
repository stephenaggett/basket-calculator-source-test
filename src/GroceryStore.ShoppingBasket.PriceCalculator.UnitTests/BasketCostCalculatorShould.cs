using System;
using FluentAssertions;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers;
using Moq;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.UnitTests
{
    public class BasketCostCalculatorShould
    {
        [Fact]
        public void ThrowInvalidOperationExceptionWhenBasketIsEmpty()
        {
            var class_under_test =
                GivenABasketCostCalculatorWithNoGoodsOrSpecialOffers();
            
            var ex = Assert.Throws<CannotCheckoutEmptyBasketException>(() => class_under_test.CalculateCost(Array.Empty<string>()));
            ex.Message.Should().Be("Cannot checkout an empty basket.");
        }

        [Fact]
        public void ThrowGoodsNotFoundExceptionWhenOneOrMoreItemsAreNotFound()
        {
            var class_under_test =
                GivenABasketCostCalculatorWithNoGoodsOrSpecialOffers();
            var ex = Assert.Throws<GoodsNotFoundException>(() => class_under_test.CalculateCost("Milk", "Apples"));
            ex.Message.Should().Be("The goods milk, apples could not be found.");
        }

        [Fact]
        public void ReturnSameSubtotalAndTotalWhenNoOffersApplied()
        {
            var class_under_test =
                GivenABasketCostCalculatorWithGoods(new[]
                    {new Good() {Name = "Milk", UnitOfMeasurement = "bottle", GbpPrice = 1.3m}});

            var result = class_under_test.CalculateCost("Milk", "Milk");
            result.SubTotal.Should().Be(2.6m);
            result.AppliedOffers.Should().BeEmpty();
            result.Total.Should().Be(2.6m);
        }
        
        [Fact]
        public void ReturnDifferentSubtotalAndTotalWhenOfferApplied()
        {
            var class_under_test =
                GivenABasketCostCalculatorWithGoodsAndOffer(new[]
                    {new Good() {Name = "Milk", UnitOfMeasurement = "bottle", GbpPrice = 1.3m}});

            var result = class_under_test.CalculateCost("Milk", "Milk");
            result.SubTotal.Should().Be(2.6m);
            result.AppliedOffers.Should().ContainSingle().Which.OfferDescription.Should().Be("10% off milk");
            result.Total.Should().Be(2.34m);
        }

        [Fact]
        public void NotBeCaseSensitiveOnGoodName()
        {
            var class_under_test =
                GivenABasketCostCalculatorWithGoods(new[]
                    {new Good() {Name = "Milk", UnitOfMeasurement = "bottle", GbpPrice = 1.3m}});

            var result = class_under_test.CalculateCost("milk");
            result.SubTotal.Should().Be(1.3m);
            result.AppliedOffers.Should().BeEmpty();
            result.Total.Should().Be(1.3m);
        }
        
        private static BasketCostCalculator GivenABasketCostCalculatorWithNoGoodsOrSpecialOffers()
        {
            return new BasketCostCalculator(Mock.Of<IGoodRepository>(), Mock.Of<ISpecialOfferPipeline>());
        }
        
        private static BasketCostCalculator GivenABasketCostCalculatorWithGoods(Good[] goods)
        {
            return new BasketCostCalculator(GivenAGoodRepositoryReturning(goods), Mock.Of<ISpecialOfferPipeline>());
        }
        
        private static BasketCostCalculator GivenABasketCostCalculatorWithGoodsAndOffer(Good[] goods)
        {
            var specialOfferPipeline = new Mock<ISpecialOfferPipeline>();
            specialOfferPipeline.Setup(sop => sop.Process(It.IsAny<Basket>())).Returns(new[]
                {new AppliedOffer() {OfferDescription = "10% off milk", TotalDiscountGbp = 0.26m}});
            return new BasketCostCalculator(GivenAGoodRepositoryReturning(goods), specialOfferPipeline.Object);
        }

        private static IGoodRepository GivenAGoodRepositoryReturning(Good[] goods)
        {
            var goodRepository = new Mock<IGoodRepository>();
            goodRepository.Setup(gr => gr.GetAll()).Returns(goods);
            return goodRepository.Object;
        }
    }
}