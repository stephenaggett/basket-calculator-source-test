using System;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers;

namespace GroceryStore.ShoppingBasket.PriceCalculator.AcceptanceTests
{
    /// <summary>
    /// This is the same as the production SpecialOfferPipelineFactory except the apples offer does not expire.
    /// </summary>
    public class TestSpecialOfferPipelineFactory : ISpecialOfferPipelineFactory
    {
        private static readonly ISpecialOfferPipeline _default =
            new SpecialOfferPipeline(new[]{
                ThisWeeksApplesOffer(), BeanOnToastOffer()});

        private static ISpecialOffer BeanOnToastOffer()
        {
            return new BuyItemsGetDiscountOnAnotherItemSpecialOffer("Beans", 2, "Bread", 0.5m);
        }

        private static ISpecialOffer ThisWeeksApplesOffer()
        {
            return new TimeLimitedPercentageDiscountSpecialOffer("Apples", 
                new DateTime(2020, 9, 14).ToUniversalTime(),
                new DateTime(2099, 9, 20).ToUniversalTime(), 
                0.1m);
        }

        public ISpecialOfferPipeline Default => _default;
    }
}