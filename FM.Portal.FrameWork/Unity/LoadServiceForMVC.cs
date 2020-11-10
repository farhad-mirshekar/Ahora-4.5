using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Domain;
using FM.Portal.Infrastructure.DAL;
using Unity;
using System.Web.Mvc;
using FM.Portal.Core.Owin;
using FM.Portal.FrameWork.Caching;
using System.Web;
using Unity.Injection;
using FM.Portal.Core.Common;
using FM.Portal.FrameWork.Email;
using FM.Portal.Core.Common.Serializer;
using Unity.Lifetime;

namespace FM.Portal.FrameWork.Unity
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IAttachmentDataSource, AttachmentDataSource>();
            container.RegisterType<IAttachmentService, AttachmentService>();

            container.RegisterType<IUserDataSource, UserDataSource>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IFaqGroupDataSource, FaqGroupDataSource>();
            container.RegisterType<IFaqGroupService, FaqGroupService>();

            container.RegisterType<IFaqDataSource, FaqDataSource>();
            container.RegisterType<IFaqService, FaqService>();

            container.RegisterType<IProductDataSource, ProductDataSource>();
            container.RegisterType<IProductService, ProductService>();

            container.RegisterType<IShoppingCartItemDataSource, ShoppingCartItemDataSource>();
            container.RegisterType<IShoppingCartItemService, ShoppingCartItemService>();

            container.RegisterType<IProductVariantAttributeDataSource, ProductVariantAttributeDataSource>();
            container.RegisterType<IProductVariantAttributeService, ProductVariantAttributeService>();

            container.RegisterType<IProductMapAttributeDataSource, ProductMapAttributeDataSource>();
            container.RegisterType<IProductMapAttributeService, ProductMapAttributeService>();

            container.RegisterType<IProductVariantAttributeDataSource, ProductVariantAttributeDataSource>();
            container.RegisterType<IProductVariantAttributeService, ProductVariantAttributeService>();

            container.RegisterType<IOrderDataSource, OrderDataSource>();
            container.RegisterType<IOrderService, OrderService>();

            container.RegisterType<IOrderDetailDataSource, OrderDetailDataSource>();

            container.RegisterType<IUserAddressDataSource, UserAddressDataSource>();
            container.RegisterType<IUserAddressService, UserAddressService>();

            container.RegisterType<IPaymentDataSource, PaymentDataSource>();
            container.RegisterType<IPaymentService, PaymentService>();

            container.RegisterType<IArticleDataSource, ArticleDataSource>();
            container.RegisterType<IArticleService, ArticleService>();

            container.RegisterType<IRefreshTokenDataSource, RefreshTokenDataSource>();
            container.RegisterType<IRefreshTokenService, RefreshTokenService>();

            container.RegisterType<ICategoryMapDiscountDataSource, CategoryMapDiscountDataSource>();
            container.RegisterType<ICategoryMapDiscountService, CategoryMapDiscountService>();

            container.RegisterType<DataSource.Ptl.ICategoryDataSource, Infrastructure.DAL.Ptl.CategoryDataSource>();
            container.RegisterType<Core.Service.Ptl.ICategoryService, Domain.Ptl.CategoryService>();

            container.RegisterType<INewsDataSource, NewsDataSource>();
            container.RegisterType<INewsService, NewsService>();

            container.RegisterType<IMenuDataSource, MenuDataSource>();
            container.RegisterType<IMenuService, MenuService>();

            container.RegisterType<ISliderDataSource, SliderDataSource>();
            container.RegisterType<ISliderService, SliderService>();

            container.RegisterType<IGeneralSettingDataSource, GeneralSettingDataSource>();
            container.RegisterType<IGeneralSettingService, GeneralSettingService>();

            container.RegisterType<IEventsDataSource, EventsDataSource>();
            container.RegisterType<IEventsService, EventsService>();

            container.RegisterType<ITagsDataSource, TagsDataSource>();
            container.RegisterType<ITagsService, TagsService>();

            container.RegisterType<IBankDataSource, BankDataSource>();
            container.RegisterType<IBankService, BankService>();

            container.RegisterType<IDownloadDataSource, DownloadDataSource>();
            container.RegisterType<IDownloadService, DownloadService>();

            container.RegisterType<IContactDataSource, ContactDataSource>();
            container.RegisterType<IContactService, ContactService>();

            container.RegisterType<DataSource.Ptl.IPagesDataSource, Infrastructure.DAL.Ptl.PagesDataSource>();
            container.RegisterType<Core.Service.Ptl.IPagesService, Domain.Ptl.PagesService>();

            container.RegisterType<IDynamicPageDataSource, DynamicPageDataSource>();
            container.RegisterType<IDynamicPageService, DynamicPageService>();

            container.RegisterType<ILinkDataSource, LinkDataSource>();
            container.RegisterType<ILinkService, LinkService>();

            container.RegisterType<IStaticPageDataSource, StaticPageDataSource>();
            container.RegisterType<IStaticPageService, StaticPageService>();

            container.RegisterType<IBannerDataSource, BannerDataSource>();
            container.RegisterType<IBannerService, BannerService>();

            container.RegisterType<IGalleryDataSource, GalleryDataSource>();
            container.RegisterType<IGalleryService, GalleryService>();

            container.RegisterType<IShippingCostDataSource, ShippingCostDataSource>();
            container.RegisterType<IShippingCostService, ShippingCostService>();

            container.RegisterType<IDeliveryDateDataSource, DeliveryDateDataSource>();
            container.RegisterType<IDeliveryDateService, DeliveryDateService>();

            container.RegisterType<IRelatedProductDataSource, RelatedProductDataSource>();
            container.RegisterType<IRelatedProductService, RelatedProductService>();

            container.RegisterType<ICategoryDataSource, CategoryDataSource>();
            container.RegisterType<ICategoryService, CategoryService>();

            container.RegisterType<IEmailLogsDataSource, EmailLogsDataSource>();
            container.RegisterType<IEmailLogsService, EmailLogsService>();

            container.RegisterType<IDocumentFlowDataSource, DocumentFlowDataSource>();
            container.RegisterType<IDocumentFlowService, DocumentFlowService>();

            container.RegisterType<ISalesDataSource, SalesDataSource>();
            container.RegisterType<ISalesService, SalesService>();

            container.RegisterType<IRoleDataSource, RoleDataSource>();
            container.RegisterType<IRoleService, RoleService>();

            container.RegisterType<IPositionDataSource, PositionDataSource>();
            container.RegisterType<IPositionService, PositionService>();

            container.RegisterType<IDepartmentDataSource, DepartmentDataSource>();
            container.RegisterType<IDepartmentService, DepartmentService>();

            container.RegisterType<ILanguageDataSource, LanguageDataSource>();
            container.RegisterType<ILanguageService, LanguageService>();

            container.RegisterType<ILocaleStringResourceDataSource, LocaleStringResourceDataSource>();
            container.RegisterType<ILocaleStringResourceService, LocaleStringResourceService>();

            container.RegisterType<IMenuItemDataSource, MenuItemDataSource>();
            container.RegisterType<IMenuItemService, MenuItemService>();

            container.RegisterType<IArticleCommentDataSource, ArticleCommentDataSource>();
            container.RegisterType<IArticleCommentService, ArticleCommentService>();

            container.RegisterType<INewsCommentDataSource, NewsCommentDataSource>();
            container.RegisterType<INewsCommentService, NewsCommentService>();

            container.RegisterType<IEventsCommentDataSource, EventsCommentDataSource>();
            container.RegisterType<IEventsCommentService, EventsCommentService>();

            container.RegisterType<IProductCommentDataSource, ProductCommentDataSource>();
            container.RegisterType<IProductCommentService, ProductCommentService>();

            container.RegisterType<IWorkContext, WebWorkContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<ICompareProductService, CompareProductService>();
            container.RegisterType<ICacheService, CacheService>();

            container.RegisterType<HttpContextBase>(new InjectionFactory(_ =>
                new HttpContextWrapper(HttpContext.Current)));

            container.RegisterType<IRequestInfo, RequestInfo>();
            container.RegisterType<IAppSetting, AppSetting>();
            container.RegisterType<IObjectSerializer, ObjectSerializer>();

            container.RegisterType<IAuthenticationService, FormsAuthenticationService>();
            container.RegisterType<IPdfService, PdfService>();

            DependencyResolver.SetResolver(new Unity.UnityDependencyResolver(container));
        }
    }
}
