using System.IO;
using System.Threading.Tasks;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Domain;
using Newtonsoft.Json;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public interface IGoodRepository
    {
        Task<Good[]> GetAllAsync();
    }

    public class GoodRepository : IGoodRepository
    {
        private readonly IConfigSettings _configSettings;

        public GoodRepository(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public async Task<Good[]> GetAllAsync()
        {
            return JsonConvert.DeserializeObject<Good[]>(
                await File.ReadAllTextAsync(_configSettings.AllGoodsForSaleJsonPath));
        }
    }
}