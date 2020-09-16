using Newtonsoft.Json;

namespace FM.Portal.Core.Common.Serializer
{
    public class ObjectSerializer : IObjectSerializer
    {
        public T DeSerialize<T>(string serializedValue)
        => JsonConvert.DeserializeObject<T>(serializedValue);

        public string Serialize(object obj)
        => JsonConvert.SerializeObject(obj);
    }
}
