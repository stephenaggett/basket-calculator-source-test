using System.Linq;
using FluentAssertions;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers;
using Moq;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.UnitTests
{
    public class SpecialOfferPipelineShould
    {
        [Fact]
        public void NotDoAnythingWhenThereAreZeroOffers()
        {
            var class_under_test = new SpecialOfferPipeline(new ISpecialOffer[]{});
            var appliedOffers = class_under_test.Process(new Basket());
            appliedOffers.Should().BeEmpty();
        }

        [Fact]
        public void ApplyMultipleOffersConsecutively()
        {
            var offer1 = GivenAnOffer("First offer", 1m);
            var offer2 = GivenAnOffer("Second offer", 2m);
            var offer3 = GivenAnOffer("Third offer", 2m, false);

            var class_under_test = new SpecialOfferPipeline(new[]{offer1, offer2, offer3});
            var result = class_under_test.Process(new Basket());
            
            result.Should().HaveCount(2);
            result.ElementAt(0).OfferDescription.Should().Be("First offer");
            result.ElementAt(0).TotalDiscountGbp.Should().Be(1m);
            result.ElementAt(1).OfferDescription.Should().Be("Second offer");
            result.ElementAt(1).TotalDiscountGbp.Should().Be(2m);
        }

        private static ISpecialOffer GivenAnOffer(string offerDescription, decimal totalDiscountGbp, bool appliesToBasket = true)
        {
            var offer = new Mock<ISpecialOffer>();
            offer.Setup(o => o.AppliesTo(It.IsAny<Basket>())).Returns(appliesToBasket);
            offer.Setup(o => o.ApplyOfferTo(It.IsAny<Basket>())).Returns(new AppliedOffer()
                {OfferDescription = offerDescription, TotalDiscountGbp = totalDiscountGbp});
            return offer.Object;
        }
    }
}