using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Domain;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/MenuItem")]
    public class MenuItemController : BaseApiController<MenuItemService>
    {
        public MenuItemController(MenuItemService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<MenuItem> Add(MenuItem model)
        => _service.Add(model);


        [HttpPost, Route("Edit")]
        public Result<MenuItem> Edit(MenuItem model)
        => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<MenuItem>> List(MenuItemListVM listVM)
       => _service.List(listVM);

        [HttpPost, Route("Get/{ID:guid}")]
        public Result<MenuItem> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);
    }
}