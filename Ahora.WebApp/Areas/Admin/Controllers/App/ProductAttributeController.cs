using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class ProductAttributeController : Controller
    {
        // GET: Admin/ProductAttribute
        public ActionResult Index()
        {
            return View();
        }
    }
}