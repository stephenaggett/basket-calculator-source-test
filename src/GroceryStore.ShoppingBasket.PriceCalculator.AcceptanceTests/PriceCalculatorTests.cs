using System.IO;
using System.Reflection;
using FluentAssertions;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Config;
using Moq;
using Xunit;

namespace GroceryStore.ShoppingBasket.PriceCalculator.AcceptanceTests
{
    public class WhenICheckOutWithItems
    {
        [Fact]
        public void ApplesSpecialOfferShouldBeApplied()
        {
            var class_under_test = GivenAPriceCalculator();
            var result = class_under_test.Checkout("Apples", "Milk", "Bread");
            result.Should().Be(@"Subtotal: £3.10
Apples 10% off: -10p
Total: £3.00");
        }
        
        [Fact]
        public void BeansOnToastSpecialOfferShouldBeApplied()
        {
            var class_under_test = GivenAPriceCalculator();
            var result = class_under_test.Checkout("Beans", "Beans", "Bread");
            result.Should().Be(@"Subtotal: £2.10
Buy 2 cans of beans and get a loaf of bread for half price: -40p
Total: £1.70");
        }
        
        [Fact]
        public void BuyingOnlyMilkShouldNotActivateOffers()
        {
            var class_under_test = GivenAPriceCalculator();
            var result = class_under_test.Checkout("Milk", "Milk");
            result.Should().Be(@"Subtotal: £2.60
(No offers available)
Total: £2.60");
        }

        private static IPriceCalculator GivenAPriceCalculator()
        {
            var configSettings = CreateConfigSettings();
            var basketCostCalculator = new BasketCostCalculator(new GoodRepository(configSettings), new SpecialOfferPipelineFactory().Default);
            return new Lib.PriceCalculator(basketCostCalculator, new ReceiptWriter());
        }

        private static IConfigSettings CreateConfigSettings()
        {
            var configSettings = new Mock<IConfigSettings>();
            configSettings.Setup(cs => cs.AllGoodsForSaleJsonPath)
                .Returns(CreateResourceFilePath("all-goods-for-sale.json"));
            return configSettings.Object;
        }

        private static string CreateResourceFilePath(string resourceFilename)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), $"Resources/{resourceFilename}");
        }
    }
}