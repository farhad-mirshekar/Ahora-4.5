using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
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
        [HttpPost,Route("List")]
        public IHttpActionResult List()
        {
            try
            {
                var result = _service.List();
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
        [HttpPost, Route("GetExcel")]
        public IHttpActionResult GetExcel()
        {
            try
            {
                var result = _service.GetExcel();
                string fileName = Guid.NewGuid().ToString();
                string filePath = System.Web.HttpContext.Current.Server.MapPath($"~/Files/Report-Excel/" + fileName + ".xlsx");
                System.IO.File.WriteAllBytes(filePath, result.Data);

                return Ok(new { Success = true, Data = new { FilePath = "/Files/Report-Excel/" + fileName + ".xlsx" } });
            }
            catch (Exception e) { return NotFound(); }
        }
    }
}
