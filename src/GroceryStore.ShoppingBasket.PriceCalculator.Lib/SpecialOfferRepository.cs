using System.IO;
using System.Threading.Tasks;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Domain;
using Newtonsoft.Json;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public interface ISpecialOfferRepository
    {
        Task<SpecialOffer[]> GetAllAsync();
    }

    public class SpecialOfferRepository : ISpecialOfferRepository
    {
        private readonly IConfigSettings _configSettings;

        public SpecialOfferRepository(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public async Task<SpecialOffer[]> GetAllAsync()
        {
            return JsonConvert.DeserializeObject<SpecialOffer[]>(
                await File.ReadAllTextAsync(_configSettings.AllSpecialOffersJsonPath));
        }
    }
}