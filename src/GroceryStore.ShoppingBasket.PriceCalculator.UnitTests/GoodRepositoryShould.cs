using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Domain;
using Moq;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.UnitTests
{
    public class GoodRepositoryShould
    {
        [Fact]
        public async Task LoadTwoGoods()
        {
            var class_under_test = new GoodRepository(CreateConfigSettings());
            var result = await class_under_test.GetAllAsync();
            result.Should().HaveCount(2);
            result.ElementAt(0).Description.Should().Be("Beans");
            result.ElementAt(0).GbpPrice.Should().Be(0.65m);
            result.ElementAt(0).UnitOfMeasurement.Should().Be("can");
        }
        
        private static IConfigSettings CreateConfigSettings()
        {
            var configSettings = new Mock<IConfigSettings>();
            configSettings.Setup(cs => cs.AllGoodsForSaleJsonPath)
                .Returns(CreateResourceFilePath("two-good-for-sale.json"));
            return configSettings.Object;
        }

        private static string CreateResourceFilePath(string resourceFilename)
        {
            return Path.Combine(Assembly.GetExecutingAssembly().Location, $"Resources/{resourceFilename}");
        }
    }
}