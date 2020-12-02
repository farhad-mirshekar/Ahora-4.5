using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Unity;
using Unity;

namespace FM.Portal.FrameWork.Attributes
{
    /// <summary>
    /// جهت نمایش متن کلاس ها
    /// </summary>
   public class DisplayNameAttribute:System.ComponentModel.DisplayNameAttribute
    {
        private readonly string _resourceKey="";
        private ILocaleStringResourceService _resourceService;
        public DisplayNameAttribute(string resourceKey)
        {
            _resourceKey = resourceKey;
        }

        public override string DisplayName
        {
            get
            {
                if (_resourceService == null)
                {
                    var container = new UnityContainer();
                    var unityDependencyResolver = new UnityDependencyResolver(container);
                    _resourceService = (ILocaleStringResourceService)unityDependencyResolver.GetService(typeof(ILocaleStringResourceService));
                }

                var resourceResult = _resourceService.GetResource(_resourceKey);
                if (resourceResult.Success)
                    return resourceResult.Data;

                return null;
            }
        }
    }
}
