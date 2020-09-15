using System;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket
{
    public class Basket
    {
        public Good[] Goods { get; set; } = {};
        
        public DateTime CheckoutDateUtc { get; set; }
    }
}