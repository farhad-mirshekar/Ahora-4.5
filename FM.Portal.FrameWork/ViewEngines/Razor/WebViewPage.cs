﻿using FM.Portal.Core.Common;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Domain;
using FM.Portal.FrameWork.Localization;
using FM.Portal.FrameWork.Unity;
using FM.Portal.Infrastructure.DAL;
using System;
using System.Web;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace FM.Portal.FrameWork.ViewEngines.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private ILocaleStringResourceService _resourceService;
        private Localizer _localizer;

        /// <summary>
        /// Get a localized resources
        /// </summary>
        public Localizer T
        {
            get
            {
                if (_resourceService == null)
                {
                    var container = new UnityContainer();
                    var unityDependencyResolver = new UnityDependencyResolver(container);
                    _resourceService = (ILocaleStringResourceService)unityDependencyResolver.GetService(typeof(ILocaleStringResourceService));
                }

                if (_localizer == null)
                {
                    //null localizer
                    //_localizer = (format, args) => new LocalizedString((args == null || args.Length == 0) ? format : string.Format(format, args));

                    //default localizer
                    _localizer = (format, args) =>
                    {
                        var resFormatResult = _resourceService.GetResource(format);
                        if (!resFormatResult.Success)
                            new LocalizedString((args == null || args.Length == 0)
                                                    ? resFormatResult.Data
                                                    : string.Format(resFormatResult.Data, args));

                        if (string.IsNullOrEmpty(resFormatResult.Data))
                        {
                            return new LocalizedString(format);
                        }
                        return
                            new LocalizedString((args == null || args.Length == 0)
                                                    ? resFormatResult.Data
                                                    : string.Format(resFormatResult.Data, args));
                    };
                }
                return _localizer;
            }
        }
    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}
