using System;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public class CannotCheckoutEmptyBasketException : Exception
    {
        public CannotCheckoutEmptyBasketException(string message) : base(message)
        {
        }
    }
}