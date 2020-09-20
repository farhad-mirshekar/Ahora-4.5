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
    [RoutePrefix("api/v1/article")]
    public class ArticleController : BaseApiController<IArticleService>
    {
        public ArticleController(IArticleService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public IHttpActionResult Add(Article model)
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
        public IHttpActionResult Edit(Article model)
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
        public IHttpActionResult List(ArticleListVM listVM)
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

        [HttpPost, Route("TypeShowArticle")]
        public IHttpActionResult TypeShowArticle()
        {
            try
            {
                var result = EnumExtensions.GetValues<ShowArticleType>();
                if (result.Count > 0)
                    return Ok(Result<List<EnumCast>>.Successful(data: result));
                else
                    return Ok(Result<List<EnumCast>>.Failure());
            }
            catch { return NotFound(); }
        }
        [HttpPost, Route("TypeCommentArticle")]
        public IHttpActionResult TypeCommentArticle()
        {
            try
            {
                var result = EnumExtensions.GetValues<CommentArticleType>();
                if (result.Count > 0)
                    return Ok(Result<List<EnumCast>>.Successful(data: result));
                else
                    return Ok(Result<List<EnumCast>>.Failure());
            }
            catch { return NotFound(); }
        }
        [HttpPost,Route("Remove/{ID:guid}")]
        public IHttpActionResult Remove(Guid ID)
        {
            try
            {
                var result = _service.Delete(ID);
                return Ok(result);
            }
            catch { return NotFound(); }
        }
    }
}
