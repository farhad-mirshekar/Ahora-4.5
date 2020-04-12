using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
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