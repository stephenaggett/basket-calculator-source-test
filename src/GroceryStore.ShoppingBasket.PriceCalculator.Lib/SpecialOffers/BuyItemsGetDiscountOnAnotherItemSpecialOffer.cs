using System;
using System.Linq;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers
{
    public class BuyItemsGetDiscountOnAnotherItemSpecialOffer : ISpecialOffer
    {
        private string RequiredGoodName { get; }
        private int RequiredGoodCount { get; }
        private string TargetGoodName { get; }
        private decimal PercentageDiscount { get; }

        public BuyItemsGetDiscountOnAnotherItemSpecialOffer(string requiredGoodName, int requiredGoodCount,
            string targetGoodName, decimal percentageDiscount)
        {
            RequiredGoodName = requiredGoodName;
            RequiredGoodCount = requiredGoodCount;
            TargetGoodName = targetGoodName;
            PercentageDiscount = percentageDiscount;
        }

        public bool AppliesTo(Basket.Basket basket)
        {
            return basket.Goods.Count(IsRequiredGood) >=
                   RequiredGoodCount &&
                   basket.Goods.Any(IsTargetGood);
        }

        private bool IsTargetGood(Good g)
        {
            return g.Name.Equals(TargetGoodName, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsRequiredGood(Good g)
        {
            return g.Name.Equals(RequiredGoodName, StringComparison.OrdinalIgnoreCase);
        }

        public AppliedOffer ApplyOfferTo(Basket.Basket basket)
        {
            var allRequiredGoods = basket.Goods.Where(IsRequiredGood).ToArray();
            var numberOfDiscountableItems = (int) (allRequiredGoods.Length / 2d);
            var allTargetGoods = basket.Goods.Where(IsTargetGood).ToArray();
            var numberOfGoodsEligibleForDiscount = Math.Min(allTargetGoods.Length, numberOfDiscountableItems);
            var totalDiscountGbp = allTargetGoods.Take(numberOfGoodsEligibleForDiscount).Select(g => g.GbpPrice * PercentageDiscount).Sum();
            return new AppliedOffer()
            {
                TotalDiscountGbp = totalDiscountGbp,
                OfferDescription = GenerateDescription(allRequiredGoods.First(), allTargetGoods.First())
            };
        }

        private string GenerateDescription(Good requiredGood, Good targetGood)
        {
            var discountDesc = PercentageDiscount == 0.5m ? "for half price" : $"with {(int)(PercentageDiscount * 100)}% off";
            return
                $"Buy {RequiredGoodCount} {requiredGood.UnitOfMeasurement.ToLower()}s of {requiredGood.Name.ToLower()} and get a {targetGood.UnitOfMeasurement.ToLower()} of {targetGood.Name.ToLower()} {discountDesc}";
        }
    }
}