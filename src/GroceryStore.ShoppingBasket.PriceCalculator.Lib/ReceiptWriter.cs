using System.Linq;
using System.Text;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public interface IReceiptWriter
    {
        string WriteFor(BasketCost basketCost);
    }

    public class ReceiptWriter : IReceiptWriter
    {
        public string WriteFor(BasketCost basketCost)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Subtotal: {AsCurrency(basketCost.SubTotal)}");

            if (!basketCost.AppliedOffers.Any())
                strBuilder.AppendLine("(No offers available)");
            
            foreach (var appliedOffer in basketCost.AppliedOffers)
            {
                strBuilder.AppendLine($"{appliedOffer.OfferDescription}: {AsCurrency(appliedOffer.TotalDiscountGbp * -1)}");
            }

            strBuilder.Append($"Total: {AsCurrency(basketCost.Total)}");
            return strBuilder.ToString();
        }

        private static string AsCurrency(decimal gbpPrice)
        {
            if (gbpPrice > -1 && gbpPrice < 1)
            {
                var numberOfPence = (int)(gbpPrice * 100);
                return $"{numberOfPence}p";
            }

            return $"{gbpPrice:C}";
        }
    }
}