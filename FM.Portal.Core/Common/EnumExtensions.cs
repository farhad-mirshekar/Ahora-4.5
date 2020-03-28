using System.Collections.Generic;
namespace FM.Portal.Core.Common
{
    public static class EnumExtensions
    {
        public static List<Model.EnumCast> GetValues<T>()
        {
            List<Model.EnumCast> values = new List<Model.EnumCast>();
            foreach (var item in System.Enum.GetValues(typeof(T)))
            {
                var item_new = item.ToString();
                item_new = item_new.Contains("_") ? item_new.Replace('_', ' ') : item_new;
                values.Add(new Model.EnumCast()
                {
                    Name = item_new,
                    Model = (byte)item
                });
            }

            return values;

        }
    }
}
