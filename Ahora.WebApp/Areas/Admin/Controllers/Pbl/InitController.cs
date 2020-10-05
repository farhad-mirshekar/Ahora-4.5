using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class InitController : Controller
    {
        // GET: Admin/Init
        public ActionResult Index()
        {
            return View();
        }
    }
}