using System;
using System.ComponentModel;
using System.Configuration;

namespace FM.Portal.Core.Common
{
    public class AppSetting : IAppSetting
    {
        public Guid ApplicationID => Guid.Parse(GetValue<string>("ApplicationID"));

        private static T GetValue<T>(string key)
        {
            var value = (object)ConfigurationManager.AppSettings[key];

            if (value is T variable)
                return variable;

            try
            {
                if (Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
                }

                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
