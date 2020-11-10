using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/ProductComment")]
    public class ProductCommentController : BaseApiController<IProductCommentService>
    {
        public ProductCommentController(IProductCommentService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public Result<ProductComment> Add(ProductComment model)
        => _service.Add(model);


        [HttpPost, Route("Edit")]
        public Result<ProductComment> Edit(ProductComment model)
        => _service.Edit(model);

        [HttpPost, Route("List")]
        public Result<List<ProductComment>> List(ProductCommentListVM listVM)
        => _service.List(listVM);


        [HttpPost, Route("Get/{ID:guid}")]
        public Result<ProductComment> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);

    }
}