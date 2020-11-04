using Ahora.WebApp.Models;
using Ahora.WebApp.Models.Ptl.Events;
using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.AutoMapper;
using FM.Portal.FrameWork.MVC.Controller;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class EventsController : BaseController<IEventsService>
    {
        private readonly IAttachmentService _attachmentService;
        public EventsController(IEventsService service
                                ,IAttachmentService attachmentService) : base(service)
        {
            _attachmentService = attachmentService;
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
        [Route("EventsDetail/{TrackingCode}/{Seo}")]
        public ActionResult Detail(string TrackingCode, string Seo)
        {
            if (!string.IsNullOrEmpty(TrackingCode))
            {
                var eventDetail = new EventModel();
                var eventResult = _service.Get(TrackingCode);
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
            return View("Error");
        }
    }
}