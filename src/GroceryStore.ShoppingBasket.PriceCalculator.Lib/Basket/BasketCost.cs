using System;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket
{
    public class BasketCost
    {
        public decimal SubTotal { get; set; }

        public AppliedOffer[] AppliedOffers { get; set; } = Array.Empty<AppliedOffer>();
        
        public decimal Total { get; set; }
    }
}