using FM.Portal.Core.Service;
using System;
using ptl = FM.Portal.Core.Service.Ptl;
using System.Web.Mvc;
using PagedList;

namespace Ahora.WebApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _service;
        private readonly ptl.ICategoryService _categoryService;

        public ArticleController(IArticleService service
                                 , ptl.ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }
        // GET: Article
        public ActionResult Index(int? page)
        {
            ViewBag.Title = "لیست مقالات";
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            var result = _service.List(4);
            return View(result.Data.ToPagedList(pageNumber, pageSize));
        }

        //show detail article
        public ActionResult Detail(string TrackingCode,string Seo)
        {
            if (TrackingCode != null && TrackingCode != string.Empty)
            {
                var result = _service.Get(TrackingCode);
                if (result.Success && result.Data.ID != Guid.Empty)
                    return View(result.Data);
            }
            return View("Error");
        }
    }
}