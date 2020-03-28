using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ahora.WebApp.Areas.User.Controllers
{
    public class AddressController : BaseController<IUserAddressService>
    {
        private readonly IUserAddressService _service;
        public AddressController(IUserAddressService service) : base(service)
        {
            _service = service;
        }

        // GET: User/Address
        public ActionResult Index(int? page)
        {
            var result = _service.List(SQLHelper.CheckGuidNull(User.Identity.Name));
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            if(result.Data != null)
            {
                if(result.Data.Count < pageNumber)
                {
                    // Create new url
                    string url = Request.Path;

                    return Redirect(url); // redirect
                }
                else
                    return View(result.Data.ToPagedList(pageNumber, pageSize));
            }
            else
                return View(result.Data);
        }
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(UserAddress model)
        {
            _service.Add(model);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(Guid ID)
        {
           var model= _service.Get(ID);
            return View(model.Data);
        }
        [HttpPost]
        public ActionResult Edit(UserAddress model)
        {
            _service.Edit(model);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(Guid ID)
        {
            _service.Remove(ID);
            return Json(true);
        }

    }
}