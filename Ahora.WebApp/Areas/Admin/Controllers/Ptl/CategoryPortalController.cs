using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class CategoryPortalController : Controller
    {
        // GET: Admin/CategoryPortal
        public ActionResult Index()
        {
            return View();
        }
    }
}