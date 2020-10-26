using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Caching;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Web;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class CommonController : BaseController<ILanguageService>
    {
        private readonly ICacheService _cacheService;
        public CommonController(ILanguageService service
                                , ICacheService cacheService) : base(service)
        {
            _cacheService = cacheService;
        }

        [ChildActionOnly]
        public ActionResult LanguageSelector()
        {
            var languageResult = _service.List(new FM.Portal.Core.Model.LanguageListVM() { PageSize = 5, PageIndex = 1 });
            if (!languageResult.Success)
                return Content("");
            var languageList = languageResult.Data;

            return PartialView("_PartialLanguageSelector", languageList);
        }
        public ActionResult ChangeLanguage(Guid LanguageID)
        {
            string redirect = "";
            var languageResult = _service.Get(LanguageID);
            if (!languageResult.Success)
                redirect = Url.RouteUrl("Home");
            var language = languageResult.Data;

            if (language != null)
            {
                var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.Language}";
                var myCookie = new HttpCookie(cookieName, null);
                myCookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Response.Cookies.Add(myCookie);
                HttpContext.Response.Cookies[cookieName].Value = language.ID.ToString();
                HttpContext.Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(1);
            }

            redirect = Url.RouteUrl("Home");
            return Redirect(redirect);
        }
    }
}