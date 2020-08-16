using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class SalesController : Controller
    {
        // GET: Admin/Sales
        public ActionResult Index()
        {
            return View();
        }
    }
}