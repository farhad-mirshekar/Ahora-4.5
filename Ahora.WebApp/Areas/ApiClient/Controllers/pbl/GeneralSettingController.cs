using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Api.Controller;
using FM.Portal.FrameWork.Caching;
using System.Web.Http;

namespace Ahora.WebApp.Areas.ApiClient.Controllers
{
    [RoutePrefix("api/v1/generalSetting")]
    public class GeneralSettingController : BaseApiController<IGeneralSettingService>
    {
        private readonly ICacheService _cacheService; 
        public GeneralSettingController(IGeneralSettingService service
                                        ,ICacheService cacheService) : base(service)
        {
            _cacheService = cacheService;
        }
        [Route("GetSetting"),HttpPost]
        public IHttpActionResult GetSetting()
        {
            try
            {
                var result = _service.GetSetting();
                return Ok(result);
            }
            catch { return NotFound(); }
        }

        [Route("Edit"),HttpPost]
        public IHttpActionResult Edit(SettingVM model)
        {
            try
            {
                var result = _service.Edit(model);
                _cacheService.RemoveSiteSettings();
                _cacheService.GetSiteSettings();
                return Ok(result);
            }
            catch { return NotFound(); }
        }
    }
}