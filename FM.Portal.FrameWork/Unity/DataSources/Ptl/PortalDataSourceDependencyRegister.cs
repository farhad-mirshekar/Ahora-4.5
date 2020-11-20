using FM.Portal.DataSource;
using FM.Portal.DataSource.Ptl;
using FM.Portal.Infrastructure.DAL;
using FM.Portal.Infrastructure.DAL.Ptl;
using Unity;

namespace FM.Portal.FrameWork.Unity.DataSources
{
   public static class PortalDataSourceDependencyRegister
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<IArticleDataSource, ArticleDataSource>();
            container.RegisterType<IArticleCommentDataSource, ArticleCommentDataSource>();
            container.RegisterType<IBannerDataSource, BannerDataSource>();
            container.RegisterType<DataSource.Ptl.ICategoryDataSource, Infrastructure.DAL.Ptl.CategoryDataSource>();
            container.RegisterType<IDynamicPageDataSource, DynamicPageDataSource>();
            container.RegisterType<IEventsDataSource, EventsDataSource>();
            container.RegisterType<IEventsCommentDataSource, EventsCommentDataSource>();
            container.RegisterType<IGalleryDataSource, GalleryDataSource>();
            container.RegisterType<INewsDataSource, NewsDataSource>();
            container.RegisterType<INewsCommentDataSource, NewsCommentDataSource>();
            container.RegisterType<DataSource.Ptl.IPagesDataSource, Infrastructure.DAL.Ptl.PagesDataSource>();
            container.RegisterType<ISliderDataSource, SliderDataSource>();
            container.RegisterType<IStaticPageDataSource, StaticPageDataSource>();
        }
    }
}
