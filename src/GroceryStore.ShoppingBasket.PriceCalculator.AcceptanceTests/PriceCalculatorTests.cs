using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Domain;
using Moq;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.AcceptanceTests
{
    public class WhenICheckOutWithItems
    {
        [Fact]
        public void ApplesSpecialOfferShouldBeApplied()
        {
            var class_under_test = CreatePriceCalculator();
            var result = class_under_test.PriceBasket("Apple", "Milk", "Bread");
            result.Should().Be(@"Subtotal: £3.10
Apples 10% off: -10p
Total: £3.00");
        }
        
        [Fact]
        public void BuyingOnlyMilkShouldNotActivateOffers()
        {
            var class_under_test = CreatePriceCalculator();
            var result = class_under_test.PriceBasket("Milk", "Milk");
            result.Should().Be(@"Subtotal: £2.60
(No offers available)
Total: £2.60");
        }

        private static Lib.PriceCalculator CreatePriceCalculator()
        {
            var configSettings = CreateConfigSettings();
            return new Lib.PriceCalculator(new GoodRepository(configSettings),  new SpecialOfferRepository(configSettings));
        }

        private static IConfigSettings CreateConfigSettings()
        {
            var configSettings = new Mock<IConfigSettings>();
            configSettings.Setup(cs => cs.AllGoodsForSaleJsonPath)
                .Returns(CreateResourceFilePath("all-goods-for-sale.json"));
            configSettings.Setup(cs => cs.AllSpecialOffersJsonPath).Returns(
                CreateResourceFilePath("all-offers.json"));
            return configSettings.Object;
        }

        private static string CreateResourceFilePath(string resourceFilename)
        {
            return Path.Combine(Assembly.GetExecutingAssembly().Location, $"Resources/{resourceFilename}");
        }
    }
}