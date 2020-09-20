using FM.Portal.Core.Service;
using System;
using ptl = FM.Portal.Core.Service.Ptl;
using System.Web.Mvc;
using PagedList;
using FM.Portal.FrameWork.MVC.Controller;
using FM.Portal.Core.Common;
using Ahora.WebApp.Models;
using System.Linq;

namespace Ahora.WebApp.Controllers
{
    public class ArticleController : BaseController<IArticleService>
    {
        private readonly ptl.ICategoryService _categoryService;
        public ArticleController(IArticleService service
                                ,ptl.ICategoryService categoryService) : base(service)
        {
            _categoryService = categoryService;
        }

        // GET: Article
        public ActionResult Index(int? page)
        {
            ViewBag.Title = "لیست مقالات";
            var articlesResult = _service.List(new FM.Portal.Core.Model.ArticleListVM() {PageSize = Helper.CountShowArticle , PageIndex = page });
            if (!articlesResult.Success)
                return View("Error");
            var articles = articlesResult.Data;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = articles.Select(x => x.Total).First();
            pageInfo.ItemsPerPage = Helper.CountShowArticle;
            
            ViewBag.Paging = pageInfo;
            return View(articles);
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