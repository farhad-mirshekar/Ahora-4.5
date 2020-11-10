using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/NewsComment")]
    public class NewsCommentController : BaseApiController<INewsCommentService>
    {
        public NewsCommentController(INewsCommentService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<NewsComment> Add(NewsComment model)
        => _service.Add(model);


        [HttpPost, Route("Edit")]
        public Result<NewsComment> Edit(NewsComment model)
        => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<NewsComment>> List(NewsCommentListVM listVM)
        => _service.List(listVM);


        [HttpPost, Route("Get/{ID:guid}")]
        public Result<NewsComment> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);

    }
}