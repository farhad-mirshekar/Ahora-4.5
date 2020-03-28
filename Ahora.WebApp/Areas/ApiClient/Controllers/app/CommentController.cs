using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;
using FM.Portal.Core.Common;
using FM.Portal.Core.Result;
using System.Collections.Generic;
namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/comment")]
    public class CommentController : BaseApiController<ICommentService>
    {
        public CommentController(ICommentService service) : base(service)
        {
        }

        [HttpPost, Route("List")]
        public IHttpActionResult List()
        {
            try
            {
                var result = _service.ListCommentForProduct();
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

        [HttpPost, Route("CommentStatusType")]
        public IHttpActionResult DiscountType()
        {
            var CommentStatusType = EnumExtensions.GetValues<CommentType>();
            var result = Result<List<EnumCast>>.Successful(data: CommentStatusType);
            return Ok(result);
        }
        [HttpPost, Route("Edit")]
        public IHttpActionResult Edit(Comment model)
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
    }
}
