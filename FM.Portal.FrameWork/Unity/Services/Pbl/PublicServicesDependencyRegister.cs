using FM.Portal.Core.Service;
using FM.Portal.Domain;
using Unity;

namespace FM.Portal.FrameWork.Unity.Services
{
   public static class PublicServicesDependencyRegister
    {
        public static void RegisterType(UnityContainer container)
        {
            container.RegisterType<IActivityLogService, ActivityLogService>();
            container.RegisterType<IActivityLogTypeService, ActivityLogTypeService>();
            container.RegisterType<IAttachmentService, AttachmentService>();
            container.RegisterType<IContactService, ContactService>();
            container.RegisterType<IDocumentFlowService, DocumentFlowService>();
            container.RegisterType<IDownloadService, DownloadService>();
            container.RegisterType<IEmailLogsService, EmailLogsService>();
            container.RegisterType<IGeneralSettingService, GeneralSettingService>();
            container.RegisterType<ILanguageService, LanguageService>();
            container.RegisterType<ILinkService, LinkService>();
            container.RegisterType<ILocaleStringResourceService, LocaleStringResourceService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<IMenuItemService, MenuItemService>();
            container.RegisterType<INotificationService, NotificationService>();
            container.RegisterType<IPagesService, PagesService>();
            container.RegisterType<ITagsService, TagsService>();
            container.RegisterType<IUrlRecordService, UrlRecordService>();
            container.RegisterType<IPdfService, PdfService>();
        }
    }
}
