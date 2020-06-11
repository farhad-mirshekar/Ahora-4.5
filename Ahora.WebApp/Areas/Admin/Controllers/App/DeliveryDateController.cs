using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class DeliveryDateController : Controller
    {
        // GET: Admin/DeliveryDate
        public ActionResult Index()
        {
            return View();
        }
    }
}