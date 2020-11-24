using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/StaticPage")]
    public class StaticPageController : BaseApiController<IStaticPageService>
    {
        public StaticPageController(IStaticPageService service) : base(service)
        {
        }

        [HttpPost, Route("Edit")]
        public Result<StaticPage> Edit(StaticPage model)
        => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<StaticPage>> List(StaticPageListVM listVM)
         => _service.List(listVM);

        [HttpPost, Route("Get/{ID:guid}")]
        public Result<StaticPage> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);

    }
}
