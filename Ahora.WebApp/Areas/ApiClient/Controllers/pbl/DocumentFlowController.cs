using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/DocumentFlow")]
    public class DocumentFlowController : BaseApiController<IDocumentFlowService>
    {
        public DocumentFlowController(IDocumentFlowService service) : base(service)
        {
        }

        [HttpPost, Route("SetAsRead/{DocumentID:guid}")]
        public IHttpActionResult SetAsRead(Guid DocumentID)
        {
            try
            {
                var result = _service.SetFlowRead(DocumentID);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }
    }
}
