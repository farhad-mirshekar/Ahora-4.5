using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class PositionController : Controller
    {
        // GET: Admin/Position
        public ActionResult Index()
        {
            return View();
        }
    }
}