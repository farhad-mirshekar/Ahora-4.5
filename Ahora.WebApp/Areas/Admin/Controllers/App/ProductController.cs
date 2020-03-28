using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cartable()
        {
            return View();
        }
    }
}