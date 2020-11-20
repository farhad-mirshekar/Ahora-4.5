using FM.Portal.DataSource;
using FM.Portal.Infrastructure.DAL;
using Unity;

namespace FM.Portal.FrameWork.Unity.DataSources
{
   public static class PublicDataSourceDependencyRegister
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<IActivityLogDataSource, ActivityLogDataSource>();
            container.RegisterType<IActivityLogTypeDataSource, ActivityLogTypeDataSource>();
            container.RegisterType<IAttachmentDataSource, AttachmentDataSource>();
            container.RegisterType<IContactDataSource, ContactDataSource>();
            container.RegisterType<IDocumentFlowDataSource, DocumentFlowDataSource>();
            container.RegisterType<IDownloadDataSource, DownloadDataSource>();
            container.RegisterType<IEmailLogsDataSource, EmailLogsDataSource>();
            container.RegisterType<IGeneralSettingDataSource, GeneralSettingDataSource>();
            container.RegisterType<ILanguageDataSource, LanguageDataSource>();
            container.RegisterType<ILinkDataSource, LinkDataSource>();
            container.RegisterType<ILocaleStringResourceDataSource, LocaleStringResourceDataSource>();
            container.RegisterType<IMenuDataSource, MenuDataSource>();
            container.RegisterType<IMenuItemDataSource, MenuItemDataSource>();
            container.RegisterType<INotificationDataSource, NotificationDataSource>();
            container.RegisterType<IPagesDataSource, PagesDataSource>();
            container.RegisterType<ITagsDataSource, TagsDataSource>();
            container.RegisterType<IUrlRecordDataSource, UrlRecordDataSource>();
        }
    }
}
