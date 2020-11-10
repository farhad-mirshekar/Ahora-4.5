using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/EventsComment")]
    public class EventsCommentController : BaseApiController<IEventsCommentService>
    {
        public EventsCommentController(IEventsCommentService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<EventsComment> Add(EventsComment model)
        => _service.Add(model);


        [HttpPost, Route("Edit")]
        public Result<EventsComment> Edit(EventsComment model)
        => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<EventsComment>> List(EventsCommentListVM listVM)
        => _service.List(listVM);


        [HttpPost, Route("Get/{ID:guid}")]
        public Result<EventsComment> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);

    }
}