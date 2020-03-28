namespace FM.Portal.FrameWork.MVC.Controller
{
   public class BaseController<T> : System.Web.Mvc.Controller
        where T: Core.Service.IService
    {
        public BaseController(T service)
        {
            _service = service;
        }
        protected readonly T _service;
    }
}
