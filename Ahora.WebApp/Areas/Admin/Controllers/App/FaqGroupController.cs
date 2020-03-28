using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    public partial class FaqGroupController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}