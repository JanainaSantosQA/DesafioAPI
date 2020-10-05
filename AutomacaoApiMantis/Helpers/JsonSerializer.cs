using Newtonsoft.Json;
using RestSharp.Serializers;

namespace AutomacaoApiMantis.Helpers
{
    public class JsonSerializer : ISerializer
    {
        private string contentType = "application/json";
        public string ContentType { get => contentType; set => contentType = value; }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}