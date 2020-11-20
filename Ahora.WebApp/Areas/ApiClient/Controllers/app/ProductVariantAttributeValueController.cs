using FM.Portal.Core;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/ProductVariantAttributeValue")]
    public class ProductVariantAttributeValueController : BaseApiController<IProductVariantAttributeValueService>
    {
        public ProductVariantAttributeValueController(IProductVariantAttributeValueService service) : base(service)
        {
        }
        [HttpPost, Route("Add")]
        public Result<ProductVariantAttributeValue> Add(ProductVariantAttributeValue model)
        => _service.Add(model);

        [HttpPost, Route("Edit")]
        public Result<ProductVariantAttributeValue> Edit(ProductVariantAttributeValue model)
        => _service.Edit(model);


        [HttpPost, Route("List/{ProductVariantAttributeID:guid}")]
        public Result<List<ProductVariantAttributeValue>> List(Guid ProductVariantAttributeID)
        => _service.List(ProductVariantAttributeID);


        [HttpPost, Route("Get/{ID:guid}")]
        public Result<ProductVariantAttributeValue> Get(Guid ID)
        => _service.Get(ID);

        [HttpPost, Route("Delete/{ID:guid}")]
        public Result Delete(Guid ID)
        => _service.Delete(ID);

    }
}
