using Ahora.WebApp.Models;
using Ahora.WebApp.Models.Ptl;
using FM.Portal.Core.Common;
using FM.Portal.Core.Infrastructure;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.AutoMapper;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class NewsController : BaseController<INewsService>
    {
        private readonly IAttachmentService _attachmentService;
        private readonly INewsCommentService _newsCommentService;
        private readonly IWorkContext _workContext;
        public NewsController(INewsService service
                             , IAttachmentService attachmentService
                             , INewsCommentService newsCommentService
                             , IWorkContext workContext) : base(service)
        {
            _attachmentService = attachmentService;
            _newsCommentService = newsCommentService;
            _workContext = workContext;
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
            pageInfo.ItemsPerPage = Helper.CountShowNews;

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
        public ActionResult Detail(Guid ID)
        {
            var newsDetail = new NewsModel();
            var newsResult = _service.Get(ID);
            if (!newsResult.Success)
                return View("Error");

            var news = newsResult.Data;
            newsDetail = news.ToModel();

            var attachmentsResult = _attachmentService.List(newsDetail.ID);
            if (attachmentsResult.Success)
            {
                newsDetail.VideoAttachments = attachmentsResult.Data.Where(a => a.PathType == PathType.video).ToList();
                newsDetail.PictureAttachments = attachmentsResult.Data.Where(a => a.PathType == PathType.news).ToList();

            }

            return View(newsDetail);
        }

        #region News Comment
        public ActionResult Comment(Guid NewsID)
        {
            var newsResult = _service.Get(NewsID);
            if (!newsResult.Success)
                return Content("");

            var news = newsResult.Data;
            if (news.CommentStatusType == CommentStatusType.بسته ||
                news.CommentStatusType == CommentStatusType.نامشخص)
                return Content("");

            var commentsResult = _newsCommentService.List(new NewsCommentListVM() { NewsID = NewsID, ShowChildren = true, CommentType = CommentType.تایید });
            if (!commentsResult.Success)
                return Content("");

            var comments = commentsResult.Data;
            var newsCommentListModel = new NewsCommentListModel();
            newsCommentListModel.AvailableComments = comments;
            newsCommentListModel.User = _workContext.User;
            newsCommentListModel.News = news;

            return PartialView("~/Views/News/Partial/_PartialComment.cshtml", newsCommentListModel);

        }

        [HttpGet]
        public ActionResult AddNewsComment(Guid NewsID, Guid? ParentID)
        {
            var errors = new Error();
            errors.Code = 500;
            errors.ErorrDescription = "خطای ناشناخته";

            var newsCommentModel = new NewsCommentModel();
            newsCommentModel.NewsID = NewsID;

            if (_workContext.User == null)
            {
                errors.Code = 404;
                errors.ErorrDescription = "مهمان گرامی! برای ارسال نظر نیاز است وارد سایت شوید";
                return PartialView("~/Views/News/Partial/_PartialError.cshtml", errors);
            }
            var commentsResult = _newsCommentService.List(new NewsCommentListVM() { NewsID = NewsID, UserID = _workContext.User.ID, CommentType = CommentType.در_حال_بررسی });

            if (!commentsResult.Success)
                return PartialView("~/Views/News/Partial/_PartialError.cshtml");

            if (commentsResult.Data.Any())
            {
                errors.Code = 1;
                errors.ErorrDescription = "شما برای این خبر یک نظر تایید نشده دارید";
                return PartialView("~/Views/News/Partial/_PartialError.cshtml", errors);
            }
            else
            {
                if (ParentID.HasValue)
                    newsCommentModel.ParentID = ParentID;

                return PartialView("~/Views/News/Partial/_PartialModify.cshtml", newsCommentModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddNewsComment(NewsCommentModel model)
        {
            if (_workContext.User == null)
                return Json(new { show = "true" });

            model.UserID = _workContext.User.ID;

            var result = _newsCommentService.Add(model.ToEntity());
            if (!result.Success)
                return Json(new { show = "true" });

            return Json(new { show = "false" });
        }

        public JsonResult Like(Guid CommentID)
        {
            if (_workContext.User == null)
                return Json(new { result = "login", CommentID = CommentID });

            var userCanLikeResult = _newsCommentService.UserCanLike(new NewsCommentMapUser() { CommentID = CommentID, UserID = _workContext.User.ID });
            if (!userCanLikeResult.Success)
                return Json(new { result = "duplicate", CommentID = CommentID });

            var LikeResult = _newsCommentService.Like(CommentID, _workContext.User.ID);
            if (!LikeResult.Success)
                return Json(new { result = "error", CommentID = CommentID });


            return Json(new { result = "success", CommentID = CommentID, state = "like", count = LikeResult.Data.LikeCount });

        }
        public JsonResult DisLike(Guid CommentID)
        {
            if (_workContext.User == null)
                return Json(new { result = "login", CommentID = CommentID });

            var userCanLikeResult = _newsCommentService.UserCanLike(new NewsCommentMapUser() { CommentID = CommentID, UserID = _workContext.User.ID });
            if (!userCanLikeResult.Success)
                return Json(new { result = "duplicate", CommentID = CommentID });

            var disLikeResult = _newsCommentService.DisLike(CommentID, _workContext.User.ID);
            if (!disLikeResult.Success)
                return Json(new { result = "error", CommentID = CommentID });


            return Json(new { result = "success", CommentID = CommentID, state = "dislike", count = disLikeResult.Data.DisLikeCount });

        }
        #endregion
    }
}