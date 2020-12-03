using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Unity;

namespace FM.Portal.FrameWork.Attributes
{
    /// <summary>
    /// جهت الزام کردن یک فیلد برای فرم ها و نمایش پیغام اخطار بر اساس زبان
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property , AllowMultiple =false)]
   public class RequiredAttribute: ValidationAttribute,IClientValidatable
    {
       private readonly string _resourceKey;
        private ILocaleStringResourceService _resourceService;
        public RequiredAttribute(string resourceKey)
        {
            _resourceKey = resourceKey;
        }
        public override string FormatErrorMessage(string name)
        {
            if (_resourceService == null)
            {
                var container = new UnityContainer();
                var unityDependencyResolver = new UnityDependencyResolver(container);
                _resourceService = (ILocaleStringResourceService)unityDependencyResolver.GetService(typeof(ILocaleStringResourceService));
            }

            var resourceResult = _resourceService.GetResource(_resourceKey);
            if (resourceResult.Success)
                return string.Format("{0}", resourceResult.Data);

            return string.Format("{0}", "*");
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(_resourceKey);
            rule.ValidationType = "required";
            yield return rule;
        }
    }
}
