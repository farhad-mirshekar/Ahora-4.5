using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class BannerController : Controller
    {
        // GET: Admin/Banner
        public ActionResult Index()
        {
            return View();
        }
    }
}