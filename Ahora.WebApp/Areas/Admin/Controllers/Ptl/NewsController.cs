using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        // GET: Admin/News
        public ActionResult Index()
        {
            return View();
        }
    }
}