using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class DiscountController : Controller
    {
        // GET: Admin/Discount
        public ActionResult Index()
        {
            return View();
        }
    }
}