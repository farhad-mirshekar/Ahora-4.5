using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles ="Admin")]
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