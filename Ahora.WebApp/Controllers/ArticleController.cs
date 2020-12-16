using FM.Portal.Core.Service;
using System.Web.Mvc;
using FM.Portal.FrameWork.MVC.Controller;
using FM.Portal.Core.Infrastructure;
using Ahora.WebApp.Models;
using System.Linq;
using Ahora.WebApp.Models.Ptl;
using FM.Portal.FrameWork.AutoMapper;
using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Common;

namespace Ahora.WebApp.Controllers
{
    public class ArticleController : BaseController<IArticleService>
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IArticleCommentService _articleCommentService;
        private readonly IWorkContext _workContext;
        private readonly IActivityLogService _activityLogService;
        private readonly ILocaleStringResourceService _localeStringResourceService;
        public ArticleController(IArticleService service
                                , IAttachmentService attachmentService
                                , IArticleCommentService articleCommentService
                                , IWorkContext workContext
                                , IActivityLogService activityLogService
                                , ILocaleStringResourceService localeStringResourceService) : base(service)
        {
            _attachmentService = attachmentService;
            _articleCommentService = articleCommentService;
            _workContext = workContext;
            _activityLogService = activityLogService;
            _localeStringResourceService = localeStringResourceService;
        }

        #region Article
        //GET: Article
        public ActionResult Index(int? page)
        {
            var articlesResult = _service.List(new ArticleListVM() { PageSize = Helper.CountShowArticle, PageIndex = page , ViewStatusType = ViewStatusType.نمایش });
            if (!articlesResult.Success)
                return View("Error");
            var articles = articlesResult.Data;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = articles.Select(x => x.Total).First();
            pageInfo.ItemsPerPage = Helper.CountShowArticle;

            var articleList = new ArticleListModel();

            foreach (var article in articles)
            {
                articleList.AvailableArticles.Add(article.ToModel());
            }

            articleList.AvailableArticles.ForEach(a =>
            {
                var attachmentResult = _attachmentService.List(a.ID);
                a.PictureAttachments = attachmentResult.Data;
            });
            articleList.PagingInfo = pageInfo;

            return View(articleList);
        }

        //show detail article
        public ActionResult Detail(Guid ID)
        {
            var articleDetail = new ArticleModel();
            var articleResult = _service.Get(ID);
            if (!articleResult.Success)
                return View("Error");

            var article = articleResult.Data;
            articleDetail = article.ToModel();

            var attachmentsResult = _attachmentService.List(articleDetail.ID);
            if (attachmentsResult.Success)
            {
                articleDetail.VideoAttachments = attachmentsResult.Data.Where(a => a.PathType == PathType.video).ToList();
                articleDetail.PictureAttachments = attachmentsResult.Data.Where(a => a.PathType == PathType.article).ToList();

            }

            return View(articleDetail);
        }

        #endregion

        #region Article Comment
        public ActionResult Comment(Guid ArticleID)
        {
            var articleResult = _service.Get(ArticleID);
            if (!articleResult.Success)
                return Content("");

            var articleCommentListModel = new ArticleCommentListModel();
            articleCommentListModel.User = _workContext.User;
            var article = articleResult.Data;
            if (article.CommentStatusType == CommentStatusType.بسته ||
                article.CommentStatusType == CommentStatusType.نامشخص)
                return PartialView("~/Views/Article/Partial/_PartialComment.cshtml", articleCommentListModel);

            var commentsResult = _articleCommentService.List(new ArticleCommentListVM() { ArticleID = ArticleID, ShowChildren = true, CommentType = CommentType.تایید });
            if (!commentsResult.Success)
                return Content("");

            var comments = commentsResult.Data;
            articleCommentListModel.AvailableComments = comments;
            articleCommentListModel.Article = article;

            return PartialView("~/Views/Article/Partial/_PartialComment.cshtml", articleCommentListModel);

        }

        [HttpGet]
        public ActionResult AddArticleComment(Guid ArticleID, Guid? ParentID)
        {
            var errors = new Error();
            errors.Code = 500;
            errors.ErorrDescription = "خطای ناشناخته";

            var articleCommentModel = new ArticleCommentModel();
            articleCommentModel.ArticleID = ArticleID;

            if (_workContext.User == null)
            {
                errors.Code = 404;
                errors.ErorrDescription = "برای رای دادن باید عضو سایت باشید";
                return PartialView("~/Views/Article/Partial/_PartialError.cshtml", errors);
            }
            var commentsResult = _articleCommentService.List(new ArticleCommentListVM() { ArticleID = ArticleID, UserID = _workContext.User.ID, CommentType = CommentType.در_حال_بررسی });

            if (!commentsResult.Success)
                return PartialView("~/Views/Article/Partial/_PartialError.cshtml");

            if (commentsResult.Data.Any())
            {
                errors.Code = 1;
                errors.ErorrDescription = "شما برای این مقاله یک نظر تایید نشده دارید";
                return PartialView("~/Views/Article/Partial/_PartialError.cshtml", errors);
            }
            else
            {
                if (ParentID.HasValue)
                    articleCommentModel.ParentID = ParentID;

                return PartialView("~/Views/Article/Partial/_PartialModify.cshtml", articleCommentModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddArticleComment(ArticleCommentModel model)
        {
            if (_workContext.User == null)
                return Json(new { show = "true" });

            model.UserID = _workContext.User.ID;

            var result = _articleCommentService.Add(model.ToEntity());
            _activityLogService.Add(new ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("").Data ?? $"افزودن نظر برای مقاله",
                UserID = _workContext.User.ID,
                IpAddress = "23",
                EntityID = result.Data.ID,
                EntityName = model.GetType().Name,
                SystemKeyword = "AddArticleComment"
            });
            if (!result.Success)
                return Json(new { show = "true" });

            return Json(new { show = "false" });
        }

        public JsonResult Like(Guid CommentID)
        {
            if (_workContext.User == null)
                return Json(new { result = "login", CommentID = CommentID });

            var userCanLikeResult = _articleCommentService.UserCanLike(new ArticleCommentMapUser() { CommentID = CommentID, UserID = _workContext.User.ID });
            if (!userCanLikeResult.Success)
                return Json(new { result = "duplicate", CommentID = CommentID });

            var LikeResult = _articleCommentService.Like(CommentID, _workContext.User.ID);
            if (!LikeResult.Success)
                return Json(new { result = "error", CommentID = CommentID });


            return Json(new { result = "success", CommentID = CommentID, state = "like", count = LikeResult.Data.LikeCount });

        }
        public JsonResult DisLike(Guid CommentID)
        {
            if (_workContext.User == null)
                return Json(new { result = "login", CommentID = CommentID });

            var userCanLikeResult = _articleCommentService.UserCanLike(new ArticleCommentMapUser() { CommentID = CommentID, UserID = _workContext.User.ID });
            if (!userCanLikeResult.Success)
                return Json(new { result = "duplicate", CommentID = CommentID });

            var disLikeResult = _articleCommentService.DisLike(CommentID, _workContext.User.ID);
            if (!disLikeResult.Success)
                return Json(new { result = "error", CommentID = CommentID });


            return Json(new { result = "success", CommentID = CommentID, state = "dislike", count = disLikeResult.Data.DisLikeCount });

        }
        #endregion
    }
}