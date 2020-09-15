using FluentAssertions;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.UnitTests
{
    public class ReceiptWriterShould
    {
        [Fact]
        public void WriteReceiptWhenNoOffers()
        {
            var basketCost = new BasketCost(){SubTotal = 2m, Total = 2m};
            var class_under_test = new ReceiptWriter();
            var result = class_under_test.WriteFor(basketCost);
            result.Should().Be(@"Subtotal: £2.00
(No offers available)
Total: £2.00");
        }

        [Fact]
        public void WriteReceiptIncludingOffers()
        {
            var basketCost = new BasketCost()
            {
                SubTotal = 2m, Total = 0.8m,
                AppliedOffers = new[]
                {
                    new AppliedOffer() {OfferDescription = "Apples 10% off", TotalDiscountGbp = 0.1m},
                    new AppliedOffer() {OfferDescription = "A completely unheard of deal", TotalDiscountGbp = 1.1m}
                }
            };
            var class_under_test = new ReceiptWriter();
            var result = class_under_test.WriteFor(basketCost);
            result.Should().Be(@"Subtotal: £2.00
Apples 10% off: -10p
A completely unheard of deal: -£1.10
Total: 80p");
        }
    }
}