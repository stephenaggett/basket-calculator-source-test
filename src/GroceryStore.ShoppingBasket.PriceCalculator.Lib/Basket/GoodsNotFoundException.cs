using System;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket
{
    public class GoodsNotFoundException : Exception
    {
        public GoodsNotFoundException(string message) : base(message)
        {
        }
    }
}