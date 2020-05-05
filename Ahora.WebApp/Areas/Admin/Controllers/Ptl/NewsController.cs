using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class NewsController : Controller
    {
        // GET: Admin/News
        public ActionResult Index()
        {
            return View();
        }
    }
}