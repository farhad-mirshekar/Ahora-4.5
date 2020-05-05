using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class RoleController : Controller
    {
        // GET: Admin/Role
        public ActionResult Index()
        {
            return View();
        }
    }
}