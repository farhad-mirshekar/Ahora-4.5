using FM.Portal.DataSource;
using FM.Portal.Infrastructure.DAL;
using Unity;

namespace FM.Portal.FrameWork.Unity.DataSources
{
   public static class ApplicationDataSourceDependencyRegister
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<IBankDataSource, BankDataSource>();
            container.RegisterType<ICategoryDataSource, CategoryDataSource>();
            container.RegisterType<ICategoryMapDiscountDataSource, CategoryMapDiscountDataSource>();
            container.RegisterType<IDeliveryDateDataSource, DeliveryDateDataSource>();
            container.RegisterType<IDiscountDataSource, DiscountDataSource>();
            container.RegisterType<IFaqDataSource, FaqDataSource>();
            container.RegisterType<IFaqGroupDataSource, FaqGroupDataSource>();
            container.RegisterType<IOrderDataSource, OrderDataSource>();
            container.RegisterType<IOrderDetailDataSource, OrderDetailDataSource>();
            container.RegisterType<IPaymentDataSource, PaymentDataSource>();
            container.RegisterType<IProductDataSource, ProductDataSource>();
            container.RegisterType<IProductMapAttributeDataSource, ProductMapAttributeDataSource>();
            container.RegisterType<IProductAttributeDataSource, ProductAttributeDataSource>();
            container.RegisterType<IProductCommentDataSource, ProductCommentDataSource>();
            container.RegisterType<IProductVariantAttributeValueDataSource, ProductVariantAttributeValueDataSource>();
            container.RegisterType<IRelatedProductDataSource, RelatedProductDataSource>();
            container.RegisterType<ISalesDataSource, SalesDataSource>();
            container.RegisterType<IShippingCostDataSource, ShippingCostDataSource>();
            container.RegisterType<IShoppingCartItemDataSource, ShoppingCartItemDataSource>();
        }
    }
}
