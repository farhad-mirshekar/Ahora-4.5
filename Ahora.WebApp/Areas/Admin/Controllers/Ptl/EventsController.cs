using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    public class EventsController : Controller
    {
        // GET: Admin/Events
        public ActionResult Index()
        {
            return View();
        }
    }
}