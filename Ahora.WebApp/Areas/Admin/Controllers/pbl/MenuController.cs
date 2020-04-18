﻿using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
        }
    }
}