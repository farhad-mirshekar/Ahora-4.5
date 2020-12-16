using FM.Portal.Core;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core.Service.Ptl;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/categoryPortal")]
    public class CategoryPortalController : BaseApiController<ICategoryService>
    {
        public CategoryPortalController(ICategoryService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<Category> Add(Category model)
         => _service.Add(model);


        [HttpPost, Route("Edit")]
        public Result<Category> Edit(Category model)
        => _service.Edit(model);


        [HttpPost, Route("List")]
        public Result<List<Category>> List()
            => _service.List();

        [HttpPost, Route("Get/{ID:guid}")]
        public Result<Category> Get(Guid ID)
         => _service.Get(ID);


        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);


    }
}