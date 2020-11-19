
using Ahora.WebApp.Factories;
using Unity;

namespace FM.Portal.FrameWork.Unity
{
   public static class FactoriesDependency
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<ICommonModelFactory, CommonModelFactory>();
            container.RegisterType<IProductModelFactory, ProductModelFactory>();
            container.RegisterType<IShoppingCartModelFactory, ShoppingCartModelFactory>();
        }
    }
}
