using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
        }
    }
}