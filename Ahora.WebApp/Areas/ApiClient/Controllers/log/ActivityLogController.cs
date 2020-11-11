using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/ActivityLog")]
    public class ActivityLogController : BaseApiController<IActivityLogService>
    {
        public ActivityLogController(IActivityLogService service) : base(service)
        {
        }

        [HttpPost, Route("List")]
        public Result<List<ActivityLog>> List(ActivityLogListVM listVM)
            => _service.List(listVM);

        [HttpPost, Route("Get/{ID:Guid}")]
        public Result<ActivityLog> Get(Guid ID)
            => _service.Get(ID);
    }
}
