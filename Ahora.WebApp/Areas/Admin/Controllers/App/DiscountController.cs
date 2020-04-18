using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class DiscountController : Controller
    {
        // GET: Admin/Discount
        public ActionResult Index()
        {
            return View();
        }
    }
}