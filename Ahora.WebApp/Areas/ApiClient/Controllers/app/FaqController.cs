using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/faq")]
    public class FaqController : BaseApiController<IFaqService>
    {
        public FaqController(IFaqService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public IHttpActionResult Add(FAQ model)
        {
            try
            {
                return Ok(_service.Add(model));
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(FAQ model)
        {
            try
            {
                return Ok(_service.Edit(model));
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost,Route("List/{FAQGroupID:Guid}")]
        public IHttpActionResult List(Guid FAQGroupID)
        {
            try
            {
                return Ok(_service.List(FAQGroupID));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
