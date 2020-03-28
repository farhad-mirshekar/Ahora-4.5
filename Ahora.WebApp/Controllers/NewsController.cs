using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using PagedList;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class NewsController : BaseController<INewsService>
    {
        public NewsController(INewsService service) : base(service)
        {
        }

        // GET: News
        public ActionResult Index(int? page)
        {
            ViewBag.Title = "لیست اخبار";
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var result = _service.List(4);
            return View(result.Data.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Detail(string TrackingCode , string Seo)
        {
            if (TrackingCode != null && TrackingCode != string.Empty)
            {
                var result = _service.Get(TrackingCode);
                if (result.Success)
                    return View(result.Data);
            }
            return View("Error");
        }
    }
}