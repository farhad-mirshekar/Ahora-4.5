namespace FM.Portal.Core.Common.Serializer
{
   public interface IObjectSerializer
    {
        string Serialize(object obj);
        T DeSerialize<T>(string serializedValue);
    }
}
