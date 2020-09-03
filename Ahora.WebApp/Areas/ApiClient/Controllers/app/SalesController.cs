using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/Sales")]
    public class SalesController : BaseApiController<ISalesService>
    {
        public SalesController(ISalesService service) : base(service)
        {
        }
        [HttpPost, Route("Add")]
        public IHttpActionResult Add(Sales model)
        {
            try
            {
                var result = _service.Add(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(Sales model)
        {
            try
            {
                var result = _service.Edit(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("List")]
        public IHttpActionResult List(SalesListVM listVM)
        {
            try
            {
                var result = _service.List(listVM);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("Get/{ID:guid}")]
        public IHttpActionResult Get(Guid ID)
        {
            try
            {
                var result = _service.Get(ID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost, Route("Confirm")]
        public IHttpActionResult Confirm(FlowConfirmVM confirmVM)
        {
            try
            {
                confirmVM.SendType = SendDocumentType.تایید_ارسال;
                var result = _service.Confirm(confirmVM);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        [HttpPost, Route("ListFlow/{ID:guid}")]
        public IHttpActionResult ListFlow(Guid ID)
        {
            try
            {
                var result = _service.ListFlow(ID);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }
    }
}
