using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace GroceryStore.ShoppingBasket.PriceCalculator.Lib
{
    public class LibInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISpecialOfferPipeline>()
                .UsingFactoryMethod(() => new SpecialOfferPipelineFactory().Default));
            container.Register(Classes.FromAssemblyContaining(typeof(LibInstaller))
                .Pick()
                .WithService.DefaultInterfaces()
                .LifestyleTransient());
        }
    }
}