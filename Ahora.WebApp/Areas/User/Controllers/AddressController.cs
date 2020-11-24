using Ahora.WebApp.Models;
using FM.Portal.Core.Infrastructure;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.MVC.Controller;
using System;
using System.Web.Mvc;
using System.Linq;

namespace Ahora.WebApp.Areas.User.Controllers
{
    public class AddressController : BaseController<IUserAddressService>
    {
        private readonly IWorkContext _workContext;

        public AddressController(IUserAddressService service
                                , IWorkContext workContext) : base(service)
        {
            _workContext = workContext;
        }

        // GET: User/Address
        public ActionResult Index(int? page)
        {
            var addressResult = _service.List(new UserAddressListVM() { UserID = _workContext.User.ID , PageSize = 3 , PageIndex = page });
            if (!addressResult.Success)
                return View("Error");
            var addressList = addressResult.Data;
            if (addressList.Count > 0)
            {
                var pageInfo = new PagingInfo();
                pageInfo.CurrentPage = page ?? 1;
                pageInfo.TotalItems = addressList.Select(a => a.Total).First();
                pageInfo.ItemsPerPage = 3;

                ViewBag.Paging = pageInfo;
                return View(addressList);
            }
            else
                return View(addressList);
            
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