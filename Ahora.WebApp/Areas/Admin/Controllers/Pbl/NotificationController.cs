using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class NotificationController : Controller
    {
        // GET: Admin/Notification
        public ActionResult Index()
        {
            return View();
        }
    }
}