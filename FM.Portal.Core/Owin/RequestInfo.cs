using System;
using System.Web;

namespace FM.Portal.Core.Owin
{
    public class RequestInfo : IRequestInfo
    {
        public Guid? ApplicationId
        {
            get
            {
                var val = GetValueFromToken(Claims.ApplicationId);
                if (string.IsNullOrWhiteSpace(val))
                    return null;
                return Guid.Parse(val);
            }
        }
        public Guid? DepartmentId
        {
            get
            {
                var val = GetValueFromToken(Claims.DepartmentId);
                if (string.IsNullOrWhiteSpace(val))
                    return null;
                return Guid.Parse(val);
            }
        }

        public Guid? PositionId
        {
            get
            {
                var val = GetValueFromToken(Claims.PositionId);
                if (string.IsNullOrWhiteSpace(val))
                    return null;
                return Guid.Parse(val);
            }
        }

        public Guid? UserId
        {
            get
            {
                var val = GetValueFromToken(Claims.UserId);
                if (string.IsNullOrWhiteSpace(val))
                    return null;
                return Guid.Parse(val);
            }
        }

        public string UserName
        {
            get
            {
                var val = GetValueFromToken(Claims.UserName);
                if (string.IsNullOrWhiteSpace(val))
                    return null;
                return val;
            }
        }

        protected string GetValueFromHeader(string key) => System.Web.HttpContext.Current.Request.Headers[key];
        protected string GetValueFromToken(string key)
        {
            try
            {
                var userIdentity = HttpContext.Current.User.Identity;
                var claimIdentity = userIdentity as System.Security.Claims.ClaimsIdentity;
                var claim = claimIdentity.FindFirst(key);
                return claim?.Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
