using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/article")]
    public class ArticleController : BaseApiController<IArticleService>
    {
        public ArticleController(IArticleService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<Article> Add(Article model)
         => _service.Add(model);

        [HttpPost, Route("Edit")]
        public Result<Article> Edit(Article model)
         => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<Article>> List(ArticleListVM listVM)
         => _service.List(listVM);

        [HttpPost, Route("Get/{ID:guid}")]
        public Result<Article> Get(Guid ID)
         => _service.Get(ID);

        [HttpPost,Route("Remove/{ID:guid}")]
        public Result Remove(Guid ID)
         => _service.Delete(ID);

    }
}
