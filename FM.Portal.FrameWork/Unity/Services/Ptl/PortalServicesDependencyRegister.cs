using FM.Portal.Core.Service;
using FM.Portal.Domain;
using Unity;

namespace FM.Portal.FrameWork.Unity.Services
{
   public static class PortalServicesDependencyRegister
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<IArticleService, ArticleService>();
            container.RegisterType<IArticleCommentService, ArticleCommentService>();
            container.RegisterType<IBannerService, BannerService>();
            container.RegisterType<Core.Service.Ptl.ICategoryService, Domain.Ptl.CategoryService>();
            container.RegisterType<IDynamicPageService, DynamicPageService>();
            container.RegisterType<IEventsService, EventsService>();
            container.RegisterType<IEventsCommentService, EventsCommentService>();
            container.RegisterType<IGalleryService, GalleryService>();
            container.RegisterType<INewsService, NewsService>();
            container.RegisterType<INewsCommentService, NewsCommentService>();
            container.RegisterType<Core.Service.Ptl.IPagesService, Domain.Ptl.PagesService>();
            container.RegisterType<ISliderService, SliderService>();
            container.RegisterType<IStaticPageService, StaticPageService>();
        }
    }
}
