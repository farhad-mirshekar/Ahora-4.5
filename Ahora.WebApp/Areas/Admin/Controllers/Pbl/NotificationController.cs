using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class NotificationController : Controller
    {
        // GET: Admin/Notification
        public ActionResult Index()
        {
            return View();
        }
    }
}