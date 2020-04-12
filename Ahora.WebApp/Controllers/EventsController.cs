using FM.Portal.Core.Common;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using PagedList;
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
            int pageSize = Helper.CountShowEvents;
            int pageNumber = (page ?? 1);
            var result = _service.List(Helper.CountShowEvents);
            return View(result.Data.ToPagedList(pageNumber, pageSize));
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