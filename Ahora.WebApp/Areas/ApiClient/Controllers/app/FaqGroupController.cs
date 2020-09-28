using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/faqgroup")]
    public class FaqGroupController : BaseApiController<IFaqGroupService>
    {
        public FaqGroupController(IFaqGroupService service) : base(service)
        {
        }

        [HttpPost, Route("List")]
        public IHttpActionResult List(FaqGroupListVM listVM)
        {
            return Ok(_service.List(listVM));
        }
        [HttpPost, Route("Add")]
        public IHttpActionResult Add(FAQGroup model)
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
        public IHttpActionResult Edit(FAQGroup model)
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
        [HttpPost, Route("Get/{ID:Guid}")]
        public IHttpActionResult Get(Guid ID)
        {
            try
            {

                return Ok(_service.Get(ID));
            }
            catch (Exception e) { return NotFound(); }
        }

    }
}
