﻿using FM.Portal.FrameWork.Attributes;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.Admin.Controllers
{
    [UserAuthorizeAttribute(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        // GET: Admin/Department
        public ActionResult Index()
        {
            return View();
        }
    }
}