using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public interface IPriceCalculator
    {
        string Checkout(params string[] itemsInBasket);
    }

    public class PriceCalculator : IPriceCalculator
    {
        private readonly IBasketCostCalculator _basketCostCalculator;
        private readonly IReceiptWriter _receiptWriter;

        public PriceCalculator(IBasketCostCalculator basketCostCalculator, IReceiptWriter receiptWriter)
        {
            _basketCostCalculator = basketCostCalculator;
            _receiptWriter = receiptWriter;
        }

        public string Checkout(params string[] itemsInBasket)
        {
            var basketCost = _basketCostCalculator.CalculateCost(itemsInBasket);
            return _receiptWriter.WriteFor(basketCost);
        }
    }
}