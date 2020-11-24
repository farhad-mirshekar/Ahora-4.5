using FM.Portal.Core;
using FM.Portal.Core.Model.Ptl;
using FM.Portal.Core.Service.Ptl;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/PagesPortal")]
    public class PagesPortalController : BaseApiController<IPagesService>
    {
        public PagesPortalController(IPagesService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<Pages> Add(Pages model)
         => _service.Add(model);

        [HttpPost, Route("Edit")]
        public Result<Pages> Edit(Pages model)
         => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<Pages>> List(PagesListVM listVM)
        => _service.List(listVM);

        [HttpPost, Route("Get/{ID:guid}")]
        public Result<Pages> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);

    }
}