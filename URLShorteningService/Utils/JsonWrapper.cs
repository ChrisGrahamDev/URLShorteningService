using Newtonsoft.Json;

namespace URLShorteningService.Utils
{
    public class JsonWrapper : IJsonWrapper
    {
        public T JsonDeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string JsonSerializeObject<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}