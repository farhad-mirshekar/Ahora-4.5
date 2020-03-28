using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Admin/Article
        public ActionResult Index()
        {
            return View();
        }
    }
}