using System;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket
{
    public class CannotCheckoutEmptyBasketException : Exception
    {
        public CannotCheckoutEmptyBasketException(string message) : base(message)
        {
        }
    }
}