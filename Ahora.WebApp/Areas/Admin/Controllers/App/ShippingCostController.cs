using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class ShippingCostController : Controller
    {
        // GET: Admin/ShippingCost
        public ActionResult Index()
        {
            return View();
        }
    }
}