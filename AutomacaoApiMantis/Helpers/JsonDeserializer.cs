using RestSharp;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace AutomacaoApiMantis.Helpers
{
    public class JsonDeserializer : IDeserializer
    {
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
    }
}