using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FM.Portal.FrameWork.Attributes;

namespace Ahora.WebApp.Areas.User.Controllers
{
    [UserAuthorizeAttribute(Roles ="User")]
    public class HomeController : Controller
    {
        // GET: User/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult OrderDetail()
        {
            return View();
        }
        public ActionResult Address()
        {
            return View();
        }
    }
}