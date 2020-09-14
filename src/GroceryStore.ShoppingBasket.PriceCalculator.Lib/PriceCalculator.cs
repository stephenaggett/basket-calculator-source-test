using System;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public class PriceCalculator
    {
        private readonly IGoodRepository _goodRepository;
        private readonly ISpecialOfferRepository _specialOfferRepository;

        public PriceCalculator(IGoodRepository goodRepository, ISpecialOfferRepository specialOfferRepository)
        {
            _goodRepository = goodRepository;
            _specialOfferRepository = specialOfferRepository;
        }

        public string PriceBasket(params string[] itemsInBasket)
        {
            throw new NotImplementedException();
        }
    }
}