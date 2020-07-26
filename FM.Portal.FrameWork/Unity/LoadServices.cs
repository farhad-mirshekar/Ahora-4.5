using FM.Portal.Core.Common;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Domain;
using FM.Portal.FrameWork.Caching;
using FM.Portal.Infrastructure.DAL;
using System.Web;
using Unity;
using Unity.Injection;

namespace FM.Portal.FrameWork.Unity
{
   public class LoadServices
    {
        private readonly IUnityContainer _container;
        public LoadServices(IUnityContainer container)
        {
            _container = container;
        }
        public IUnityContainer Load()
        {
            _container.RegisterType<IFaqGroupDataSource, FaqGroupDataSource>();
            _container.RegisterType<IFaqGroupService, FaqGroupService>();

            _container.RegisterType<IFaqDataSource, FaqDataSource>();
            _container.RegisterType<IFaqService, FaqService>();

            _container.RegisterType<IAttachmentDataSource, AttachmentDataSource>();
            _container.RegisterType<IAttachmentService, AttachmentService>();

            _container.RegisterType<IUserDataSource, UserDataSource>();
            _container.RegisterType<IUserService, UserService>();

            _container.RegisterType<ICommandDataSource, CommandDataSource>();
            _container.RegisterType<ICommandService, CommandService>();

            _container.RegisterType<IRoleDataSource, RoleDataSource>();
            _container.RegisterType<IRoleService, RoleService>();

            _container.RegisterType<IPositionDataSource, PositionDataSource>();
            _container.RegisterType<IPositionService, PositionService>();

            _container.RegisterType<IProductAttributeDataSource, ProductAttributeDataSource>();
            _container.RegisterType<IProductAttributeService, ProductAttributeService>();

            _container.RegisterType<IProductDataSource, ProductDataSource>();
            _container.RegisterType<IProductService, ProductService>();

            _container.RegisterType<ICategoryDataSource, CategoryDataSource>();
            _container.RegisterType<ICategoryService, CategoryService>();

            _container.RegisterType<IDiscountDataSource, DiscountDataSource>();
            _container.RegisterType<IDiscountService, DiscountService>();

            _container.RegisterType<ICommentDataSource, CommentDataSource>();
            _container.RegisterType<ICommentService, CommentService>();

            _container.RegisterType<IProductMapAttributeDataSource, ProductMapAttributeDataSource>();
            _container.RegisterType<IProductMapAttributeService, ProductMapAttributeService>();

            _container.RegisterType<IProductVariantAttributeDataSource, ProductVariantAttributeDataSource>();
            _container.RegisterType<IProductVariantAttributeService, ProductVariantAttributeService>();

            _container.RegisterType<IArticleDataSource, ArticleDataSource>();
            _container.RegisterType<IArticleService, ArticleService>();

            _container.RegisterType<IRefreshTokenDataSource, RefreshTokenDataSource>();
            _container.RegisterType<IRefreshTokenService, RefreshTokenService>();

            _container.RegisterType<FM.Portal.DataSource.Ptl.ICategoryDataSource, FM.Portal.Infrastructure.DAL.Ptl.CategoryDataSource>();
            _container.RegisterType<FM.Portal.Core.Service.Ptl.ICategoryService,FM.Portal.Domain.Ptl.CategoryService>();

            _container.RegisterType<ICategoryMapDiscountDataSource, CategoryMapDiscountDataSource>();
            _container.RegisterType<ICategoryMapDiscountService, CategoryMapDiscountService>();

            _container.RegisterType<INewsDataSource, NewsDataSource>();
            _container.RegisterType<INewsService, NewsService>();

            _container.RegisterType<IPagesDataSource, PagesDataSource>();
            _container.RegisterType<IPagesService, PagesService>();

            _container.RegisterType<IMenuDataSource, MenuDataSource>();
            _container.RegisterType<IMenuService, MenuService>();

            _container.RegisterType<ISliderDataSource, SliderDataSource>();
            _container.RegisterType<ISliderService, SliderService>();

            _container.RegisterType<IGeneralSettingDataSource, GeneralSettingDataSource>();
            _container.RegisterType<IGeneralSettingService, GeneralSettingService>();

            _container.RegisterType<IEventsDataSource, EventsDataSource>();
            _container.RegisterType<IEventsService, EventsService>();

            _container.RegisterType<ITagsDataSource, TagsDataSource>();
            _container.RegisterType<ITagsService, TagsService>();

            _container.RegisterType<IPaymentDataSource, PaymentDataSource>();
            _container.RegisterType<IPaymentService, PaymentService>();

            _container.RegisterType<IOrderDataSource, OrderDataSource>();
            _container.RegisterType<IOrderService, OrderService>();

            _container.RegisterType<IOrderDetailDataSource, OrderDetailDataSource>();

            _container.RegisterType<IUserAddressDataSource, UserAddressDataSource>();
            _container.RegisterType<IUserAddressService, UserAddressService>();

            _container.RegisterType<INotificationDataSource, NotificationDataSource>();
            _container.RegisterType<INotificationService, NotificationService>();

            _container.RegisterType<IDownloadDataSource, DownloadDataSource>();
            _container.RegisterType<IDownloadService, DownloadService>();

            _container.RegisterType<IDepartmentDataSource, DepartmentDataSource>();
            _container.RegisterType<IDepartmentService, DepartmentService>();

            _container.RegisterType<IContactDataSource, ContactDataSource>();
            _container.RegisterType<IContactService, ContactService>();

            _container.RegisterType<DataSource.Ptl.IPagesDataSource, Infrastructure.DAL.Ptl.PagesDataSource>();
            _container.RegisterType<Core.Service.Ptl.IPagesService, Domain.Ptl.PagesService>();

            _container.RegisterType<IDynamicPageDataSource, DynamicPageDataSource>();
            _container.RegisterType<IDynamicPageService, DynamicPageService>();

            _container.RegisterType<ILinkDataSource, LinkDataSource>();
            _container.RegisterType<ILinkService, LinkService>();

            _container.RegisterType<IStaticPageDataSource, StaticPageDataSource>();
            _container.RegisterType<IStaticPageService, StaticPageService>();

            _container.RegisterType<IBannerDataSource, BannerDataSource>();
            _container.RegisterType<IBannerService, BannerService>();

            _container.RegisterType<IGalleryDataSource, GalleryDataSource>();
            _container.RegisterType<IGalleryService, GalleryService>();

            _container.RegisterType<IShippingCostDataSource, ShippingCostDataSource>();
            _container.RegisterType<IShippingCostService, ShippingCostService>();

            _container.RegisterType<IDeliveryDateDataSource, DeliveryDateDataSource>();
            _container.RegisterType<IDeliveryDateService, DeliveryDateService>();

            _container.RegisterType<IRelatedProductDataSource, RelatedProductDataSource>();
            _container.RegisterType<IRelatedProductService, RelatedProductService>();

            _container.RegisterType<IEmailLogsDataSource, EmailLogsDataSource>();
            _container.RegisterType<IEmailLogsService, EmailLogsService>();

            _container.RegisterType<ICacheService, CacheService>();
            _container.RegisterType<HttpContextBase>(new InjectionFactory(_ =>
                new HttpContextWrapper(HttpContext.Current)));
            _container.RegisterType<IRequestInfo, RequestInfo>();
            _container.RegisterType<IAppSetting, AppSetting>();
            return _container;
        }
    }
}
