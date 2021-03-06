﻿using FM.Portal.Core.Infrastructure;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace FM.Portal.FrameWork
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class WebHelper : IWebHelper
    {
        #region Fields 

        private readonly HttpContextBase _httpContext;

        #endregion
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        public WebHelper(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }


        public bool IsRequestBeingRedirected => throw new NotImplementedException();

        public bool IsPostBeingDone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetCurrentIpAddress()
        {
            try
            {
                if (!IsRequestAvailable(_httpContext))
                    return string.Empty;

                var result = "";
                if (_httpContext.Request.Headers != null)
                {
                    //The X-Forwarded-For (XFF) HTTP header field is a de facto standard
                    //for identifying the originating IP address of a client
                    //connecting to a web server through an HTTP proxy or load balancer.
                    var forwardedHttpHeader = "X-FORWARDED-FOR";
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ForwardedHTTPheader"]))
                    {
                        //but in some cases server use other HTTP header
                        //in these cases an administrator can specify a custom Forwarded HTTP header
                        forwardedHttpHeader = ConfigurationManager.AppSettings["ForwardedHTTPheader"];
                    }

                    //it's used for identifying the originating IP address of a client connecting to a web server
                    //through an HTTP proxy or load balancer. 
                    string xff = _httpContext.Request.Headers.AllKeys
                        .Where(x => forwardedHttpHeader.Equals(x, StringComparison.InvariantCultureIgnoreCase))
                        .Select(k => _httpContext.Request.Headers[k])
                        .FirstOrDefault();

                    //if you want to exclude private IP addresses, then see http://stackoverflow.com/questions/2577496/how-can-i-get-the-clients-ip-address-in-asp-net-mvc
                    if (!String.IsNullOrEmpty(xff))
                    {
                        string lastIp = xff.Split(new char[] { ',' }).FirstOrDefault();
                        result = lastIp;
                    }
                }

                if (String.IsNullOrEmpty(result) && _httpContext.Request.UserHostAddress != null)
                {
                    result = _httpContext.Request.UserHostAddress;
                }

                //some validation
                if (result == "::1")
                    result = "127.0.0.1";
                //remove port
                if (!String.IsNullOrEmpty(result))
                {
                    int index = result.IndexOf(":", StringComparison.InvariantCultureIgnoreCase);
                    if (index > 0)
                        result = result.Substring(0, index);
                }
                return result;
            }
            catch(Exception e) { return "127.0.0.1"; }
           
        }

        public string GetThisPageUrl(bool includeQueryString)
        {
            bool useSsl = IsCurrentConnectionSecured();
            return GetThisPageUrl(includeQueryString, useSsl);
        }

        public string GetThisPageUrl(bool includeQueryString, bool useSsl)
        {
            string url = string.Empty;
            if (!IsRequestAvailable(_httpContext))
                return url;

            //if (includeQueryString)
            //{
            //    string storeHost = GetStoreHost(useSsl);
            //    if (storeHost.EndsWith("/"))
            //        storeHost = storeHost.Substring(0, storeHost.Length - 1);
            //    url = storeHost + _httpContext.Request.RawUrl;
            //}
            //else
            //{
                if (_httpContext.Request.Url != null)
                {
                    url = _httpContext.Request.Url.GetLeftPart(UriPartial.Path);
                }
            //}
            url = url.ToLowerInvariant();
            return url;
        }

        public string GetUrlReferrer()
        {
            string referrerUrl = string.Empty;

            //URL referrer is null in some case (for example, in IE 8)
            if (IsRequestAvailable(_httpContext) && _httpContext.Request.UrlReferrer != null)
                referrerUrl = _httpContext.Request.UrlReferrer.PathAndQuery;

            return referrerUrl;
        }

        public bool IsCurrentConnectionSecured()
        {
            bool useSsl = false;
            if (IsRequestAvailable(_httpContext))
            {
                useSsl = _httpContext.Request.IsSecureConnection;
                //when your hosting uses a load balancer on their server then the Request.IsSecureConnection is never got set to true, use the statement below
                //just uncomment it
                //useSSL = _httpContext.Request.ServerVariables["HTTP_CLUSTER_HTTPS"] == "on" ? true : false;
            }

            return useSsl;
        }
        public string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }

            //not hosted. For example, run in unit tests
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        #region Utilities
        protected virtual Boolean IsRequestAvailable(HttpContextBase httpContext)
        {
            if (httpContext == null)
                return false;

            try
            {
                if (httpContext.Request == null)
                    return false;
            }
            catch (HttpException ex)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
