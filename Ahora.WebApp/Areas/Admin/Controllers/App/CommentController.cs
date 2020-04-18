using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
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