namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib.Config
{
    public interface IConfigSettings
    {
        string AllGoodsForSaleJsonPath { get; set; }
    }
    
    public class ConfigSettings : IConfigSettings
    {
        public string AllGoodsForSaleJsonPath { get; set; }
    }
}