using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class CommandController : Controller
    {
        // GET: Admin/Command
        public ActionResult Index()
        {
            return View();
        }
    }
}