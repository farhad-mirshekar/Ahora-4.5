using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/events")]
    public class EventsController : BaseApiController<IEventsService>
    {
        public EventsController(IEventsService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<Events> Add(Events model)
         => _service.Add(model);

        [HttpPost, Route("Edit")]
        public Result<Events> Edit(Events model)
         => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<Events>> List(EventsListVM listVM)
         => _service.List(listVM);

        [HttpPost, Route("Get/{ID:guid}")]
        public Result<Events> Get(Guid ID)
         => _service.Get(ID);

        [HttpPost, Route("Remove/{ID:guid}")]
        public Result Remove(Guid ID)
         => _service.Delete(ID);
    }
}
