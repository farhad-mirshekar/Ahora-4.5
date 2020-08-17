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

        [HttpPost,Route("Confirm")]
        public IHttpActionResult Confirm(FlowConfirmVM model)
        {
            try
            {
                model.SendType = SendDocumentType.تایید_ارسال;
                var result = _service.Confirm(model);
                return Ok(result);
            }
            catch(Exception e) { return NotFound(); }
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
