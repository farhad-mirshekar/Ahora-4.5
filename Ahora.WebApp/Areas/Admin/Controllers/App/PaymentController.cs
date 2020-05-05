using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class PaymentController : Controller
    {
        // GET: Admin/Payment
        public ActionResult Index()
        {
            return View();
        }
    }
}