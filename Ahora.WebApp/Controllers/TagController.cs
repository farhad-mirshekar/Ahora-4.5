using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ahora.WebApp.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index(string Name)
        {
            return View();
        }
    }
}