using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using System;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/LocaleStringResource")]
    public class LocaleStringResourceController : BaseApiController<ILocaleStringResourceService>
    {
        public LocaleStringResourceController(ILocaleStringResourceService service) : base(service)
        {
        }

        [HttpPost, Route("Add")]
        public IHttpActionResult Add(LocaleStringResource model)
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
        public IHttpActionResult Edit(LocaleStringResource model)
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
        public IHttpActionResult List(LocaleStringResourceListVM listVM)
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
        [HttpPost, Route("Delete/{ID:guid}")]
        public IHttpActionResult Delete(Guid ID)
        {
            try
            {
                var result = _service.Delete(ID);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}