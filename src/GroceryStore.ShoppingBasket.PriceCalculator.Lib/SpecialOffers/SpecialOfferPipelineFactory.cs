using System;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public interface ISpecialOfferPipelineFactory
    {
        ISpecialOfferPipeline Default { get; }
    }

    /// <summary>
    /// This factory could be enhanced to read offer details from a repository.
    /// </summary>
    public class SpecialOfferPipelineFactory : ISpecialOfferPipelineFactory
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
                new DateTime(2020, 9, 20).ToUniversalTime(), 
                0.1m);
        }

        public ISpecialOfferPipeline Default => _default;
    }
}