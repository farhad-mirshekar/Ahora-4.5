using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class EventsController : Controller
    {
        // GET: Admin/Events
        public ActionResult Index()
        {
            return View();
        }
    }
}