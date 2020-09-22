using Ahora.WebApp.Models;
using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System.Linq;
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
            ViewBag.Title = "آلبوم اخبار";
            var newsResult = _service.List(new FM.Portal.Core.Model.NewsListVM() { PageSize = Helper.CountShowNews, PageIndex = page });
            if (!newsResult.Success)
                return View("Error");
            var news = newsResult.Data;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = news.Select(x => x.Total).First();
            pageInfo.ItemsPerPage = Helper.CountShowNews;

            ViewBag.Paging = pageInfo;
            return View(news);
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