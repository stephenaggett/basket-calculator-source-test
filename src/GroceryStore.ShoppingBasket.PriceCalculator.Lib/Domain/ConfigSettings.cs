namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib.Domain
{
    public interface IConfigSettings
    {
        string AllSpecialOffersJsonPath { get; set; }
        string AllGoodsForSaleJsonPath { get; set; }
    }
    
    public class ConfigSettings : IConfigSettings
    {
        public string AllSpecialOffersJsonPath { get; set; }
        public string AllGoodsForSaleJsonPath { get; set; }
    }
}