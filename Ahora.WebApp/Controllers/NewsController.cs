using Ahora.WebApp.Models;
using Ahora.WebApp.Models.Ptl.News;
using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.AutoMapper;
using FM.Portal.FrameWork.MVC.Controller;
using System.Linq;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class NewsController : BaseController<INewsService>
    {
        private readonly IAttachmentService _attachmentService;
        public NewsController(INewsService service
                             , IAttachmentService attachmentService) : base(service)
        {
            _attachmentService = attachmentService;
        }

        // GET: News
        public ActionResult Index(int? page)
        {
            var newsResult = _service.List(new FM.Portal.Core.Model.NewsListVM() { PageSize = Helper.CountShowNews, PageIndex = page });
            if (!newsResult.Success)
                return View("Error");
            var news = newsResult.Data;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = news.Select(x => x.Total).First();
            pageInfo.ItemsPerPage = Helper.CountShowArticle;

            news.ForEach(e =>
            {
                var attachmentResult = _attachmentService.List(e.ID);
                e.Attachments = attachmentResult.Data;
            });

            var articleList = new NewsListModel();
            articleList.AvailableNews = news;
            articleList.PagingInfo = pageInfo;

            return View(articleList);
        }
        public ActionResult Detail(string TrackingCode , string Seo)
        {
            if (!string.IsNullOrEmpty(TrackingCode))
            {
                var newsDetail = new NewsModel();
                var newsResult = _service.Get(TrackingCode);
                if (!newsResult.Success)
                    return View("Error");

                var news = newsResult.Data;
                newsDetail = news.ToModel();

                var attachmentsResult = _attachmentService.List(newsDetail.ID);
                if (attachmentsResult.Success)
                {
                    newsDetail.VideoAttachments = attachmentsResult.Data.Where(a => a.PathType == FM.Portal.Core.Model.PathType.video).ToList();
                    newsDetail.PictureAttachments = attachmentsResult.Data.Where(a => a.PathType == FM.Portal.Core.Model.PathType.news).ToList();

                }

                return View(newsDetail);

            }
            return View("Error");
        }
    }
}