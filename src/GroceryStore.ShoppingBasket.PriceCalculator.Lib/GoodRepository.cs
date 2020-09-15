using System.IO;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Config;
using Newtonsoft.Json;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public interface IGoodRepository
    {
        Good[] GetAll();
    }

    public class GoodRepository : IGoodRepository
    {
        private readonly IConfigSettings _configSettings;

        public GoodRepository(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public Good[] GetAll()
        {
            var json = File.ReadAllText(_configSettings.AllGoodsForSaleJsonPath);
            return JsonConvert.DeserializeObject<Good[]>(json);
        }
    }
}