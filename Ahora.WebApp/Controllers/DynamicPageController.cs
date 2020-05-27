using Ahora.WebApp.Models;
using FM.Portal.Core.Service.Ptl;
using FM.Portal.FrameWork.MVC.Controller;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class DynamicPageController : BaseController<IPagesService>
    {
        private readonly FM.Portal.Core.Service.IDynamicPageService _dynamicPageService;
        public DynamicPageController(IPagesService service
                                     , FM.Portal.Core.Service.IDynamicPageService dynamicPageService) : base(service)
        {
            _dynamicPageService = dynamicPageService;
        }

        // GET: DynamicPage
        public ActionResult Index(string TrackingCode , string Seo)
        {
            var pageResult = _service.Get(TrackingCode);
            if (!pageResult.Success)
                return View("Error", new Error { ClassCss = "alert alert-dange", ErorrDescription = "صفحه مورد نظر یافت نشد" });
            var page = pageResult.Data;
            var dynamicPageResult = _dynamicPageService.List(new FM.Portal.Core.Model.DynamicPageListVM {PageID=page.ID });
            if (!dynamicPageResult.Success)
                return View("Error", new Error { ClassCss = "alert alert-dange", ErorrDescription = "صفحه مورد نظر یافت نشد" });
            ViewBag.Title = page.Name;
            return View(dynamicPageResult.Data);
        }
    }
}