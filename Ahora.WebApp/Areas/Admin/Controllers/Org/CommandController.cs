using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class CommandController : Controller
    {
        // GET: Admin/Command
        public ActionResult Index()
        {
            return View();
        }
    }
}