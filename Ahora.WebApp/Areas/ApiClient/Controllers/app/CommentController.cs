using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;
using FM.Portal.Core.Common;
using FM.Portal.Core;
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
        public IHttpActionResult List(CommentListVM listVM)
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

        [HttpPost, Route("CommentStatusType")]
        public IHttpActionResult DiscountType()
        {
            var CommentStatusType = EnumExtensions.GetValues<CommentType>();
            var result = Result<List<EnumCast>>.Successful(data: CommentStatusType);
            return Ok(result);
        }
        [HttpPost, Route("CommentForType")]
        public IHttpActionResult CommentForType()
        {
            var CommentForType = EnumExtensions.GetValues<CommentForType>();
            List<EnumCast> enumCasts = new List<EnumCast>();
            foreach (var item in CommentForType)
            {
                if (item.Model != 0 && item.Model != 6 )
                    enumCasts.Add(new EnumCast {Model=item.Model , Name=item.Name });
            }
            var result = Result<List<EnumCast>>.Successful(data: enumCasts);
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
