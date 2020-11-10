using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class ProductCommentController : Controller
    {
        // GET: Admin/ProductComment
        public ActionResult Index()
        {
            return View();
        }
    }
}