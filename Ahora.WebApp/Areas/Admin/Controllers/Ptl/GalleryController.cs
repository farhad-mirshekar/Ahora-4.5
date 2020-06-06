using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class GalleryController : Controller
    {
        // GET: Admin/Gallery
        public ActionResult Index()
        {
            return View();
        }
    }
}