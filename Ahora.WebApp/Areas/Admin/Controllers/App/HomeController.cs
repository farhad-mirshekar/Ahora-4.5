using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    public partial class HomeController : Controller
    {
        // GET: Admin/Home
        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult Main()
        {
            return View();
        }
    }
}