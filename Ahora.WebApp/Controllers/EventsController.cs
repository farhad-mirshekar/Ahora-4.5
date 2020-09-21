using Ahora.WebApp.Models;
using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
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
            var eventsResult = _service.List(new FM.Portal.Core.Model.EventsListVM() { PageSize = Helper.CountShowArticle, PageIndex = page });
            if (!eventsResult.Success)
                return View("Error");
            var events = eventsResult.Data;

            var pageInfo = new PagingInfo();
            pageInfo.CurrentPage = page ?? 1;
            pageInfo.TotalItems = events.Select(x => x.Total).First();
            pageInfo.ItemsPerPage = Helper.CountShowArticle;

            ViewBag.Paging = pageInfo;
            return View(events);
        }
        [Route("EventsDetail/{TrackingCode}/{Seo}")]
        public ActionResult Detail(string TrackingCode, string Seo)
        {
            if (TrackingCode != null && TrackingCode != string.Empty)
            {
                var result = _service.Get(TrackingCode);
                if (!result.Success)
                    return View("Error");
                var events = result.Data;
                var resultVideo = _attachmentService.GetVideo(events.ID);
                if (resultVideo.Success)
                    ViewBag.video = resultVideo.Data;
                return View(events);

            }
            return View("Error");
        }
    }
}