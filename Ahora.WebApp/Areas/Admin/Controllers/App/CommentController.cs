using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class CommentController : Controller
    {
        // GET: Admin/Comment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListForPortal()
        {
            return View();
        }
    }
}