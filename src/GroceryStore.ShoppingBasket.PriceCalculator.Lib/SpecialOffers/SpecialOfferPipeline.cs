using System.Linq;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public interface ISpecialOfferPipeline
    {
        AppliedOffer[] Process(Basket.Basket basket);
    }

    public class SpecialOfferPipeline : ISpecialOfferPipeline
    {
        private readonly ISpecialOffer[] _allSpecialOffers;

        public SpecialOfferPipeline(ISpecialOffer[] allSpecialOffers)
        {
            _allSpecialOffers = allSpecialOffers;
        }
        
        public AppliedOffer[] Process(Basket.Basket basket)
        {
            return _allSpecialOffers.Where(so => so.AppliesTo(basket)).Select(so => so.ApplyOfferTo(basket)).ToArray();
        }
    }
}