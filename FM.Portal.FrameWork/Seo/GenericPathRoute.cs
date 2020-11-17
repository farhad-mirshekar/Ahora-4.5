using FM.Portal.Core.Service;
using FM.Portal.FrameWork.Localization;
using FM.Portal.FrameWork.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;

namespace FM.Portal.FrameWork.Seo
{
    /// <summary>
    /// Provides properties and methods for defining a SEO friendly route, and for getting information about the route.
    /// </summary>
    public partial class GenericPathRoute : LocalizedRoute
    {
        private IUrlRecordService _urlRecordService;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern and handler class.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class and default parameter values.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class, default parameter values and constraints.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the System.Web.Routing.Route class, using the specified URL pattern, handler class, default parameter values, 
        /// constraints,and custom values.
        /// </summary>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
        /// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
        /// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. The route handler might need these values to process the request.</param>
        /// <param name="routeHandler">The object that processes requests for the route.</param>
        public GenericPathRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <returns>
        /// An object that contains the values from the route definition.
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData data = base.GetRouteData(httpContext);
            if (_urlRecordService == null)
            {
                _urlRecordService = (IUrlRecordService)DependencyResolver.Current.GetService(typeof(IUrlRecordService));
            }

            if (data != null)
            {

                var generic_se_name = data.Values["generic_se_name"] as string;
                if(generic_se_name.ToLowerInvariant() == "refreshtoken")
                {
                    data.Values["controller"] = "Account";
                    data.Values["action"] = "RefreshToken";
                    return data;
                }
                var urlRecordResult = _urlRecordService.Get(generic_se_name);
                if (!urlRecordResult.Success)
                {
                    return data;
                }
                var urlRecord = urlRecordResult.Data;
                if(urlRecord == null || urlRecord.ID == Guid.Empty)
                {
                    data.Values["controller"] = "Common";
                    data.Values["action"] = "PageNotFound";
                    return data;
                }
                //process URL
                switch (urlRecord.EntityName.ToLowerInvariant())
                {
                    case "product":
                        {
                            data.Values["controller"] = "Product";
                            data.Values["action"] = "Index";
                            data.Values["ID"] = urlRecord.EntityID;
                            data.Values["SeName"] = urlRecord.UrlDesc;
                        }
                        break;
                    case "article":
                        {
                            data.Values["controller"] = "Article";
                            data.Values["action"] = "Detail";
                            data.Values["ID"] = urlRecord.EntityID;
                            data.Values["SeName"] = urlRecord.UrlDesc;
                        }
                        break;
                    case "event":
                        {
                            data.Values["controller"] = "Event";
                            data.Values["action"] = "Detail";
                            data.Values["ID"] = urlRecord.EntityID;
                            data.Values["SeName"] = urlRecord.UrlDesc;
                        }
                        break;
                    case "news":
                        {
                            data.Values["controller"] = "News";
                            data.Values["action"] = "Detail";
                            data.Values["ID"] = urlRecord.EntityID;
                            data.Values["SeName"] = urlRecord.UrlDesc;
                        }
                        break;
                    default:
                        {
                            //no record found

                            //generate an event this way developers could insert their own types
                           
                        }
                        break;
                }
            }
            return data;
        }

        #endregion
    }
}
