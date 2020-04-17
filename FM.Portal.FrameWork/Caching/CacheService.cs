using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using System.Web;

namespace FM.Portal.FrameWork.Caching
{
   public class CacheService : ICacheService
    {
        public const string SiteSettingsKey = "SiteSettings";

        private readonly HttpContextBase _httpContext;
        private readonly IGeneralSettingService _settingService;

        public CacheService(HttpContextBase httpContext, IGeneralSettingService settingService)
        {
            _httpContext = httpContext;
            _settingService = settingService;
        }

        public SettingVM GetSiteSettings()
        {
            var siteSettings = _httpContext.CacheRead<SettingVM>(SiteSettingsKey);

            const int durationMinutes = 240;


            if (siteSettings != null) return siteSettings;

            var result = _settingService.GetSetting();
            siteSettings = result.Data;
            _httpContext.CacheInsert(SiteSettingsKey, siteSettings, durationMinutes);

            return siteSettings;
        }

        public void RemoveSiteSettings()
        {
            _httpContext.InvalidateCache(SiteSettingsKey);
        }

    }
}
