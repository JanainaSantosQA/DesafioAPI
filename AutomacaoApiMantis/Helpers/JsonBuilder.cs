using System.IO;
using Microsoft.Extensions.Configuration;

namespace AutomacaoApiMantis.Helpers
{
    public class JsonBuilder
    {
        public static IConfigurationRoot configuration { get; set; } = null;

        public static string ReturnParameterAppSettings(string param)
        {

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();

            return configuration[param].ToString();
        }


        public static void UpdateParameterAppSettings(string parameter, string newValue)
        {
            string json = File.ReadAllText(Directory.GetCurrentDirectory() + "/appsettings.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj[parameter] = newValue;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Directory.GetCurrentDirectory() + "/appsettings.json", output);
        }

    }
}