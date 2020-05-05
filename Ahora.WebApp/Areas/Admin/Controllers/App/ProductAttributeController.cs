using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class ProductAttributeController : Controller
    {
        // GET: Admin/ProductAttribute
        public ActionResult Index()
        {
            return View();
        }
    }
}