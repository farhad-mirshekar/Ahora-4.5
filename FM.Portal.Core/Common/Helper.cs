using System;
using System.Globalization;
using System.Web;
using FM.Portal.Core.Model;
using FM.Portal.FrameWork.Caching;

namespace FM.Portal.Core.Common
{
    public static class Helper
    {
        public static string Address
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.Address;
            }
        }
        public static int CountShowArticle
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                var count = SQLHelper.CheckIntNull(result.CountShowArticle);
                return count > 0 ? count : 4;
            }
        }
        public static int CountShowNews
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                var count = SQLHelper.CheckIntNull(result.CountShowNews);
                return count > 0 ? count : 4;
            }
        }
        public static int CountShowProduct
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                var count = SQLHelper.CheckIntNull(result.CountShowProduct);
                return count > 0 ? count : 4;
            }
        }
        public static int CountShowEvents
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                var count = SQLHelper.CheckIntNull(result.CountShowEvents);
                return count > 0 ? count : 4;
            }
        }
        public static int CountShowSlider
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                var count = Convert.ToInt32(result.CountShowSlider);
                return count > 0 ? count : 4;
            }
        }
        public static string FacebookUrl
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.FacebookUrl;
            }
        }
        public static string Fax
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.Fax;
            }
        }
        public static string InstagramUrl
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.InstagramUrl;
            }
        }
        public static string Mobile
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.Mobile;
            }
        }
        public static string Phone
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.Phone;
            }
        }
        public static string SiteDescription
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.SiteDescription;
            }
        }
        public static string SiteKeyword
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.SiteKeyword;
            }
        }
        public static string SiteMetaTag
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.SiteMetaTag;
            }
        }
        public static string SiteName
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.SiteName;
            }
        }
        public static string SiteUrl
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.SiteUrl;
            }
        }
        public static string TelegramUrl
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.TelegramUrl;
            }
        }
        public static string TwitterUrl
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.TwitterUrl;
            }
        }
        public static string WhatsAppUrl
        {
            get
            {
                HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);
                var result = CacheManager.CacheRead<SettingVM>(httpContext, "SiteSettings");
                return result.WhatsAppUrl;
            }
        }
        public static int GetPersianDay(DateTime date)
        {
            return new PersianCalendar().GetDayOfMonth(date);
        }

        public static int GetPersianMonth(DateTime date)
        {
            return new PersianCalendar().GetMonth(date);
        }

        public static int GetPersianYear(DateTime date)
        {
            return new PersianCalendar().GetYear(date);
        }

        public static string GetPersianMonthName(int MonthNumber)
        {
            switch (MonthNumber)
            {
                case 1:
                    return "فروردين";
                case 2:
                    return "ارديبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تير";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهريور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    break;
            }
            return "";
        }
        public static string GetPersianDate(DateTime date)
        {
            return GetPersianYear(date).ToString() + "/" + (GetPersianMonth(date).ToString().Length == 1 ? "0" + GetPersianMonth(date).ToString() : GetPersianMonth(date).ToString()) + "/" + (GetPersianDay(date).ToString().Length == 1 ? "0" + GetPersianDay(date).ToString() : GetPersianDay(date).ToString());
        }
    }
}
