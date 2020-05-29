using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class LinkController : Controller
    {
        // GET: Admin/Link
        public ActionResult Index()
        {
            return View();
        }
    }
}