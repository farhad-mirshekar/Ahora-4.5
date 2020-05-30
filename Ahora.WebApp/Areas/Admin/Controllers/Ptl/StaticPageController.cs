using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class StaticPageController : Controller
    {
        // GET: Admin/StaticPage
        public ActionResult Index()
        {
            return View();
        }
    }
}