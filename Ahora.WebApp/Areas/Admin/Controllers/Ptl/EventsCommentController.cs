using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class EventsCommentController : Controller
    {
        // GET: Admin/EventsComment
        public ActionResult Index()
        {
            return View();
        }
    }
}