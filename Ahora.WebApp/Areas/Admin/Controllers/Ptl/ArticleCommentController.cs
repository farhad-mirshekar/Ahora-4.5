using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class ArticleCommentController : Controller
    {
        // GET: Admin/ArticleComment
        public ActionResult Index()
        {
            return View();
        }
    }
}