using System;
using System.Linq;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public interface IBasketCostCalculator
    {
        BasketCost CalculateCost(params string[] itemsInBasket);
    }

    public class BasketCostCalculator : IBasketCostCalculator
    {
        private readonly ISpecialOfferPipeline _specialOfferPipeline;
        private readonly Lazy<Good[]> _allGoodsLazy; 

        public BasketCostCalculator(IGoodRepository goodRepository, ISpecialOfferPipeline specialOfferPipeline)
        {
            _allGoodsLazy = new Lazy<Good[]>(goodRepository.GetAll);
            _specialOfferPipeline = specialOfferPipeline;
        }

        public BasketCost CalculateCost(params string[] itemsInBasket)
        {
            if (!itemsInBasket.Any())
                throw new CannotCheckoutEmptyBasketException("Cannot checkout an empty basket.");
            var allGoods = _allGoodsLazy.Value;
            var unrecognizedItems = itemsInBasket.Except(allGoods.Select(g => g.Name), StringComparer.OrdinalIgnoreCase).ToArray();
            if (unrecognizedItems.Any())
            {
                var goodsNotFoundDesc = string.Join(", ", unrecognizedItems).ToLower();
                throw new GoodsNotFoundException($"The goods {goodsNotFoundDesc} could not be found.");
            }

            var chosenGoods = itemsInBasket
                .Select(i => allGoods.Single(g => g.Name.Equals(i, StringComparison.OrdinalIgnoreCase)))
                .ToArray();
            var subTotal = chosenGoods.Select(cg => cg.GbpPrice).Sum();
            var appliedOffers = _specialOfferPipeline.Process(new Basket.Basket(){CheckoutDateUtc = DateTime.UtcNow, Goods = chosenGoods});
            return new BasketCost()
            {
                SubTotal = subTotal, 
                AppliedOffers = appliedOffers,
                Total = subTotal - appliedOffers.Select(ao => ao.TotalDiscountGbp).Sum()
            };
        }
    }
}