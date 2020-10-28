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
        private readonly IWorkContext _workContext;
        public CommonController(ILanguageService service
                                , IWorkContext workContext) : base(service)
        {
            _workContext = workContext;
        }

        [ChildActionOnly]
        public ActionResult LanguageSelector()
        {
            var languageResult = _service.List(new FM.Portal.Core.Model.LanguageListVM() { PageSize = 5, PageIndex = 1 });
            if (!languageResult.Success)
                return Content("");
            var languageList = languageResult.Data;

            languageList.ForEach(lng =>
            {
                if(_workContext.WorkingLanguage != null)
                {
                    if (lng.ID == _workContext.WorkingLanguage.ID)
                        lng.CurrentLanguage = _workContext.WorkingLanguage;
                }
            });
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
                _workContext.WorkingLanguage = language;
                language.CurrentLanguage = language;
            }

            redirect = Url.RouteUrl("Home");
            return Redirect(redirect);
        }
    }
}