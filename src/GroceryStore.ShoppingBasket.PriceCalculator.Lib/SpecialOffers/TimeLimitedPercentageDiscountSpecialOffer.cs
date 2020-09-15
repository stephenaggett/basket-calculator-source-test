using System;
using System.Linq;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers
{
    public class TimeLimitedPercentageDiscountSpecialOffer : ISpecialOffer
    {
        private string GoodName { get; }
        private DateTime StartDateUtc { get; }
        private DateTime EndDateUtc { get; }
        private decimal PercentageDiscount { get; }

        public TimeLimitedPercentageDiscountSpecialOffer(string goodName, DateTime startDateUtc, DateTime endDateUtc, decimal percentageDiscount)
        {
            GoodName = goodName;
            StartDateUtc = startDateUtc;
            EndDateUtc = endDateUtc;
            PercentageDiscount = percentageDiscount;
        }
        
        public bool AppliesTo(Basket.Basket basket)
        {
            return basket.CheckoutDateUtc >= StartDateUtc && basket.CheckoutDateUtc <= EndDateUtc && basket.Goods.Any(IsGoodEligibleForOffer);
        }

        private bool IsGoodEligibleForOffer(Good g)
        {
            return g.Name.Equals(GoodName, StringComparison.CurrentCultureIgnoreCase);
        }

        public AppliedOffer ApplyOfferTo(Basket.Basket basket)
        {
            if (!AppliesTo(basket))
                throw new InvalidOperationException("This basket is not eligible for this offer.");
            var totalDiscount = basket.Goods.Where(IsGoodEligibleForOffer).Select(g => g.GbpPrice * PercentageDiscount).Sum();
            return new AppliedOffer() {OfferDescription = GenerateDescription(), TotalDiscountGbp = totalDiscount};
        }

        private string GenerateDescription()
        {
            return $"{GoodName} {(int) (PercentageDiscount * 100)}% off";
        }
    }
}