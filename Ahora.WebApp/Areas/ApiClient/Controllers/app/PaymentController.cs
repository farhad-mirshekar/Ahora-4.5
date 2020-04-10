using FM.Bank.Core.Model;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/Payment")]
    public class PaymentController : BaseApiController<IPaymentService>
    {
        public PaymentController(IPaymentService service) : base(service)
        {
        }
        [HttpPost,Route("List/{ResCodeType}")]
        public IHttpActionResult List(ResCode ResCodeType)
        {
            try
            {
                var result = _service.List(ResCodeType);
                return Ok(result);
            }
            catch(Exception e) { return NotFound(); }
        }
        [HttpPost, Route("GetDetail/{ID:guid}")]
        public IHttpActionResult GetDetail(Guid ID)
        {
            try
            {
                var result = _service.GetDetail(ID);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }
        [HttpPost, Route("ResCodeType")]
        public IHttpActionResult ResCodeType()
        {
            try
            {
                var ResCodeType = EnumExtensions.GetValues<ResCode>();
                var result = Result<List<EnumCast>>.Successful(data: ResCodeType);
                return Ok(result);
            }
            catch (Exception e) { return NotFound(); }
        }
    }
}
