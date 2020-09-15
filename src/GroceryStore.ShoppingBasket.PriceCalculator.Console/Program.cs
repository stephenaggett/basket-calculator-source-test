using Castle.MicroKernel.Registration;
using Castle.Windsor;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Basket;
using GroceryStore.ShoppingBasket.PriceCalculator.Lib.Config;
using Microsoft.Extensions.Configuration;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var configSettings = LoadConfigSettings();

            var container = new WindsorContainer();
            container.Register(Component.For<IConfigSettings>().Instance(configSettings));
            container.Install(new LibInstaller());
            
            var priceCalculator = container.Resolve<IPriceCalculator>();

            try
            {
                var receipt = priceCalculator.Checkout(args);
                System.Console.WriteLine(receipt);
            }
            catch (GoodsNotFoundException e)
            {
                System.Console.WriteLine(e.Message);
            }
            catch (CannotCheckoutEmptyBasketException e)
            {
                System.Console.WriteLine(e.Message);
            }
            
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static IConfigSettings LoadConfigSettings()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var configSettings = new ConfigSettings();
            config.GetSection("AppSettings").Bind(configSettings);
            return configSettings;
        }
    }
}