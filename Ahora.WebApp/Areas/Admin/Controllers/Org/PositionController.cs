using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class PositionController : Controller
    {
        // GET: Admin/Position
        public ActionResult Index()
        {
            return View();
        }
    }
}