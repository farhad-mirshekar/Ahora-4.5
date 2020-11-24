using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/DynamicPage")]
    public class DynamicPageController : BaseApiController<IDynamicPageService>
    {
        public DynamicPageController(IDynamicPageService service) : base(service)
        {
        }
        [HttpPost, Route("Add")]
        public Result<DynamicPage> Add(DynamicPage model)
        => _service.Add(model);

        [HttpPost, Route("Edit")]
        public Result<DynamicPage> Edit(DynamicPage model)
        => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<DynamicPage>> List(DynamicPageListVM listVM)
        => _service.List(listVM);

        [HttpPost, Route("Get/{ID:guid}")]
        public Result<DynamicPage> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);

    }
}
