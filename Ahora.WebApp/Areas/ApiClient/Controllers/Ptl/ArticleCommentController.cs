using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/ArticleComment")]
    public class ArticleCommentController : BaseApiController<IArticleCommentService>
    {
        public ArticleCommentController(IArticleCommentService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<ArticleComment> Add(ArticleComment model)
        => _service.Add(model);


        [HttpPost, Route("Edit")]
        public Result<ArticleComment> Edit(ArticleComment model)
        => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<ArticleComment>> List(ArticleCommentListVM listVM)
        => _service.List(listVM);


        [HttpPost, Route("Get/{ID:guid}")]
        public Result<ArticleComment> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);

    }
}