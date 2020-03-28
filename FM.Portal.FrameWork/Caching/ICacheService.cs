using FM.Portal.Core.Model;

namespace FM.Portal.FrameWork.Caching
{
   public interface ICacheService
    {
        SettingVM GetSiteSettings();
        void RemoveSiteSettings();
    }
}
