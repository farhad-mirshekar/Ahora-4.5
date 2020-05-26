using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class PagesPortalController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View();
        }
    }
}