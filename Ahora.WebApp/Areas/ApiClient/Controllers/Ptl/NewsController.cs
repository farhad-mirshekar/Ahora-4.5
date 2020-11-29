using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/news")]
    public class NewsController : BaseApiController<INewsService>
    {
        public NewsController(INewsService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<News> Add(News model)
         => _service.Add(model);

        [HttpPost, Route("Edit")]
        public Result<News> Edit(News model)
         => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<News>> List(NewsListVM listVM)
         => _service.List(listVM);

        [HttpPost, Route("Get/{ID:guid}")]
        public Result<News> Get(Guid ID)
         => _service.Get(ID);

        [HttpPost, Route("Remove/{ID:guid}")]
        public Result Remove(Guid ID)
         => _service.Delete(ID);
    }
}
