using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorize(Roles = "Admin")]
    public class NewsCommentController : Controller
    {
        // GET: Admin/NewsComment
        public ActionResult Index()
        {
            return View();
        }
    }
}