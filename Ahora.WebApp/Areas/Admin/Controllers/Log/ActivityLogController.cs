using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorize(Roles = "Admin")]
    public class ActivityLogController : Controller
    {
        // GET: Admin/ActivityLog
        public ActionResult Index()
        {
            return View();
        }
    }
}