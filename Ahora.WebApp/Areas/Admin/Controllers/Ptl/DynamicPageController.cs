using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class DynamicPageController : Controller
    {
        // GET: Admin/DynamicPage
        public ActionResult Index()
        {
            return View();
        }
    }
}