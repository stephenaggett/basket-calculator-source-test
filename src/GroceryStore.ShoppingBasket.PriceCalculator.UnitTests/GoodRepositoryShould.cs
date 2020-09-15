using System.IO;
using System.Linq;
using FluentAssertions;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Config;
using Moq;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.UnitTests
{
    public class GoodRepositoryShould
    {
        [Fact]
        public void LoadTwoGoods()
        {
            var class_under_test = new GoodRepository(CreateConfigSettings());
            var result = class_under_test.GetAll();
            result.Should().HaveCount(2);
            result.ElementAt(0).Name.Should().Be("Beans");
            result.ElementAt(0).UnitOfMeasurement.Should().Be("can");
            result.ElementAt(0).GbpPrice.Should().Be(0.65m);
        }
        
        private static IConfigSettings CreateConfigSettings()
        {
            var configSettings = new Mock<IConfigSettings>();
            configSettings.Setup(cs => cs.AllGoodsForSaleJsonPath)
                .Returns(CreateResourceFilePath("two-goods-for-sale.json"));
            return configSettings.Object;
        }

        private static string CreateResourceFilePath(string resourceFilename)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), $"Resources/{resourceFilename}");
        }
    }
}