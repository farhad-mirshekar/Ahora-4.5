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
    public class EventsController : BaseController<IEventsService>
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IEventsCommentService _eventsCommentService;
        private readonly IWorkContext _workContext;
        public EventsController(IEventsService service
                                , IAttachmentService attachmentService
                                , IEventsCommentService eventsCommentService
                                , IWorkContext workContext) : base(service)
        {
            _attachmentService = attachmentService;
            _eventsCommentService = eventsCommentService;
            _workContext = workContext;
        }

        // GET: Events
        public ActionResult Index(int? page)
        {
            ViewBag.Title = "لیست رویدادها";
            var eventsModel = new EventListModel();

            var eventsResult = _service.List(new FM.Portal.Core.Model.EventsListVM() { PageSize = Helper.CountShowArticle, PageIndex = page });
            if (!eventsResult.Success)
                return View("Error");
            var events = eventsResult.Data;

            events.ForEach(e =>
            {
                var attachmentResult = _attachmentService.List(e.ID);
                e.Attachments = attachmentResult.Data;
            });

            eventsModel.AvailableEvents = events;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = events.Select(x => x.Total).First();
            pageInfo.ItemsPerPage = Helper.CountShowArticle;

            eventsModel.PagingInfo = pageInfo;

            return View(eventsModel);
        }
        public ActionResult Detail(Guid ID)
        {
            var eventDetail = new EventModel();
            var eventResult = _service.Get(ID);
            if (!eventResult.Success)
                return View("Error");

            var events = eventResult.Data;
            eventDetail = events.ToModel();

            var attachmentsResult = _attachmentService.List(eventDetail.ID);
            if (attachmentsResult.Success)
            {
                eventDetail.VideoAttachments = attachmentsResult.Data.Where(a => a.PathType == FM.Portal.Core.Model.PathType.video).ToList();
                eventDetail.PictureAttachments = attachmentsResult.Data.Where(a => a.PathType == FM.Portal.Core.Model.PathType.events).ToList();

            }

            return View(eventDetail);

        }

        #region Events Comment
        public ActionResult Comment(Guid EventsID)
        {
            var eventsResult = _service.Get(EventsID);
            if (!eventsResult.Success)
                return Content("");

            var events = eventsResult.Data;
            if (events.CommentStatusType == CommentStatusType.بسته ||
                events.CommentStatusType == CommentStatusType.نامشخص)
                return Content("");

            var commentsResult = _eventsCommentService.List(new EventsCommentListVM() { EventsID = EventsID, ShowChildren = true, CommentType = CommentType.تایید });
            if (!commentsResult.Success)
                return Content("");

            var comments = commentsResult.Data;
            var eventsCommentListModel = new EventsCommentListModel();
            eventsCommentListModel.AvailableComments = comments;
            eventsCommentListModel.User = _workContext.User;
            eventsCommentListModel.Events = events;

            return PartialView("~/Views/Events/Partial/_PartialComment.cshtml", eventsCommentListModel);

        }

        [HttpGet]
        public ActionResult AddEventsComment(Guid EventsID, Guid? ParentID)
        {
            var errors = new Error();
            errors.Code = 500;
            errors.ErorrDescription = "خطای ناشناخته";

            var eventsCommentModel = new EventsCommentModel();
            eventsCommentModel.EventsID = EventsID;

            if (_workContext.User == null)
            {
                errors.Code = 404;
                errors.ErorrDescription = "مهمان گرامی! برای ارسال نظر نیاز است وارد سایت شوید";
                return PartialView("~/Views/Events/Partial/_PartialError.cshtml", errors);
            }
            var commentsResult = _eventsCommentService.List(new EventsCommentListVM() { EventsID = EventsID, UserID = _workContext.User.ID, CommentType = CommentType.در_حال_بررسی });

            if (!commentsResult.Success)
                return PartialView("~/Views/Events/Partial/_PartialError.cshtml");

            if (commentsResult.Data.Any())
            {
                errors.Code = 1;
                errors.ErorrDescription = "شما برای این رویداد یک نظر تایید نشده دارید";
                return PartialView("~/Views/Events/Partial/_PartialError.cshtml", errors);
            }
            else
            {
                if (ParentID.HasValue)
                    eventsCommentModel.ParentID = ParentID;

                return PartialView("~/Views/Events/Partial/_PartialModify.cshtml", eventsCommentModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddEventsComment(EventsCommentModel model)
        {
            if (_workContext.User == null)
                return Json(new { show = "true" });

            model.UserID = _workContext.User.ID;

            var result = _eventsCommentService.Add(model.ToEntity());
            if (!result.Success)
                return Json(new { show = "true" });

            return Json(new { show = "false" });
        }

        public JsonResult Like(Guid CommentID)
        {
            if (_workContext.User == null)
                return Json(new { result = "login", CommentID = CommentID });

            var userCanLikeResult = _eventsCommentService.UserCanLike(new EventsCommentMapUser() { CommentID = CommentID, UserID = _workContext.User.ID });
            if (!userCanLikeResult.Success)
                return Json(new { result = "duplicate", CommentID = CommentID });

            var LikeResult = _eventsCommentService.Like(CommentID, _workContext.User.ID);
            if (!LikeResult.Success)
                return Json(new { result = "error", CommentID = CommentID });


            return Json(new { result = "success", CommentID = CommentID, state = "like", count = LikeResult.Data.LikeCount });

        }
        public JsonResult DisLike(Guid CommentID)
        {
            if (_workContext.User == null)
                return Json(new { result = "login", CommentID = CommentID });

            var userCanLikeResult = _eventsCommentService.UserCanLike(new EventsCommentMapUser() { CommentID = CommentID, UserID = _workContext.User.ID });
            if (!userCanLikeResult.Success)
                return Json(new { result = "duplicate", CommentID = CommentID });

            var disLikeResult = _eventsCommentService.DisLike(CommentID, _workContext.User.ID);
            if (!disLikeResult.Success)
                return Json(new { result = "error", CommentID = CommentID });


            return Json(new { result = "success", CommentID = CommentID, state = "dislike", count = disLikeResult.Data.DisLikeCount });

        }
        #endregion
    }
}