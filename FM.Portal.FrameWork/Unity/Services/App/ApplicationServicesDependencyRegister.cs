using FM.Portal.Core.Service;
using FM.Portal.Domain;
using Unity;

namespace FM.Portal.FrameWork.Unity.Services
{
   public static class ApplicationServicesDependencyRegister
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<IBankService, BankService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<ICategoryMapDiscountService, CategoryMapDiscountService>();
            container.RegisterType<IDeliveryDateService, DeliveryDateService>();
            container.RegisterType<IDiscountService, DiscountService>();
            container.RegisterType<IFaqService, FaqService>();
            container.RegisterType<IFaqGroupService, FaqGroupService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IPaymentService, PaymentService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductMapAttributeService, ProductMapAttributeService>();
            container.RegisterType<IProductAttributeService, ProductAttributeService>();
            container.RegisterType<IProductCommentService, ProductCommentService>();
            container.RegisterType<IProductVariantAttributeValueService, ProductVariantAttributeValueService>();
            container.RegisterType<IRelatedProductService, RelatedProductService>();
            container.RegisterType<ISalesService, SalesService>();
            container.RegisterType<IShippingCostService, ShippingCostService>();
            container.RegisterType<IShoppingCartItemService, ShoppingCartItemService>();
            container.RegisterType<ICompareProductService, CompareProductService>();
        }
    }
}
