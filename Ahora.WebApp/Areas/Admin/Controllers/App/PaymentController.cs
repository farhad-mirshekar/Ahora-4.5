using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class PaymentController : Controller
    {
        // GET: Admin/Payment
        public ActionResult Index()
        {
            return View();
        }
    }
}