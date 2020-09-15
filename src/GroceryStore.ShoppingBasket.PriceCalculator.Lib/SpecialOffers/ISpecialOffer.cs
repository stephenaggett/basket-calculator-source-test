namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib.SpecialOffers
{
    public interface ISpecialOffer
    {
        bool AppliesTo(Basket.Basket basket);
        AppliedOffer ApplyOfferTo(Basket.Basket basket);
    }
}