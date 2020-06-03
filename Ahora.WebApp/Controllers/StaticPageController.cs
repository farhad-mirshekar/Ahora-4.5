using Ahora.WebApp.Models;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class StaticPageController : BaseController<IStaticPageService>
    {
        public StaticPageController(IStaticPageService service) : base(service)
        {
        }

        // GET: StaticPage
        public ActionResult Index(string TrackingCode , string Seo)
        {
            if (string.IsNullOrEmpty(TrackingCode))
                return View("Error", new Error { ClassCss = "alert alert-dange", ErorrDescription = "صفحه مورد نظر یافت نشد" });

            var staticPageResult = _service.Get(TrackingCode);
            if (!staticPageResult.Success)
                return View("Error", new Error { ClassCss = "alert alert-dange", ErorrDescription = "صفحه مورد نظر یافت نشد" });
            var staticPage = staticPageResult.Data;

            ViewBag.Title = staticPage.Name;
            return View(staticPage);
        }
    }
}