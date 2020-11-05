using FM.Portal.Core.Service;
using System;
using ptl = FM.Portal.Core.Service.Ptl;
using System.Web.Mvc;
using PagedList;
using FM.Portal.FrameWork.MVC.Controller;
using FM.Portal.Core.Common;
using Ahora.WebApp.Models;
using System.Linq;
using Ahora.WebApp.Models.Ptl.Article;
using FM.Portal.FrameWork.AutoMapper;

namespace Ahora.WebApp.Controllers
{
    public class ArticleController : BaseController<IArticleService>
    {
        private readonly ptl.ICategoryService _categoryService;
        private readonly IAttachmentService _attachmentService;
        public ArticleController(IArticleService service
                                ,ptl.ICategoryService categoryService
                                , IAttachmentService attachmentService) : base(service)
        {
            _categoryService = categoryService;
            _attachmentService = attachmentService;
        }

        // GET: Article
        public ActionResult Index(int? page)
        {
            var articlesResult = _service.List(new FM.Portal.Core.Model.ArticleListVM() {PageSize = Helper.CountShowArticle , PageIndex = page });
            if (!articlesResult.Success)
                return View("Error");
            var articles = articlesResult.Data;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = articles.Select(x => x.Total).First();
            pageInfo.ItemsPerPage = Helper.CountShowArticle;

            articles.ForEach(e =>
            {
                var attachmentResult = _attachmentService.List(e.ID);
                e.Attachments = attachmentResult.Data;
            });

            var articleList = new ArticleListModel();
            articleList.AvailableArticles = articles;
            articleList.PagingInfo = pageInfo;

            return View(articleList);
        }

        //show detail article
        public ActionResult Detail(string TrackingCode,string Seo)
        {
            if (!string.IsNullOrEmpty(TrackingCode))
            {
                var articleDetail = new ArticleModel();
                var articleResult = _service.Get(TrackingCode);
                if (!articleResult.Success)
                    return View("Error");

                var article = articleResult.Data;
                articleDetail = article.ToModel();

                var attachmentsResult = _attachmentService.List(articleDetail.ID);
                if (attachmentsResult.Success)
                {
                    articleDetail.VideoAttachments = attachmentsResult.Data.Where(a => a.PathType == FM.Portal.Core.Model.PathType.video).ToList();
                    articleDetail.PictureAttachments = attachmentsResult.Data.Where(a => a.PathType == FM.Portal.Core.Model.PathType.article).ToList();

                }

                return View(articleDetail);

            }
            return View("Error");
        }
    }
}