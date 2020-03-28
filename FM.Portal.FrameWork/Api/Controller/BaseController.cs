using FM.Portal.Core.Service;
using System.Web.Http;

namespace FM.Portal.FrameWork.Api.Controller
{
    [FM.Portal.FrameWork.Attributes.Authorize.Authorize]
   public class BaseApiController<T> : ApiController
        where T:IService
    {
        public BaseApiController(T service)
        {
            _service = service;
        }

        protected readonly T _service;
    }
}
