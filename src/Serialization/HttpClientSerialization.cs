using Newtonsoft.Json;

namespace Http.Abstractions.Serialization
{
    public class HttpClientSerialization : ISerialize
    {
        public T DeserializeContent<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public string SerializeContent<T>(T content)
        {
            return JsonConvert.SerializeObject(content);
        }
    }    
}
