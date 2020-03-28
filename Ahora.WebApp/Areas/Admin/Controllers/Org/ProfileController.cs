using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Admin/Profile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}