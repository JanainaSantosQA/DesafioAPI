using RestSharp;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Authenticators;
using System.Collections.Generic;

namespace AutomacaoApiMantis.Helpers
{
    public class RestSharpHelpers
    {
        public static IRestResponse<dynamic> ExecuteRequest(string url,
                                                            string requestService,
                                                            Method method,
                                                            IDictionary<string, string> headers,
                                                            IDictionary<string, string> cookies,
                                                            IDictionary<string, string> parameters,
                                                            bool parameterTypeIsUrlSegment,
                                                            string jsonBody,
                                                            bool httpBasicAuthenticator,
                                                            bool ntlmAuthenticator)
        {
            RestRequest request = new RestRequest(requestService, method);

            foreach (var header in headers)
            {
                request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }

            foreach (var cookie in cookies)
            {
                request.AddParameter(cookie.Key, cookie.Value, ParameterType.Cookie);
            }

            foreach (var parameter in parameters)
            {
                if (parameterTypeIsUrlSegment)
                {
                    request.AddParameter(parameter.Key, parameter.Value, ParameterType.UrlSegment);
                }
                else
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }

            request.JsonSerializer = new JsonSerializer();

            if (jsonBody != null)
            {
                if (GeneralHelpers.IsAJsonArray(jsonBody))
                {
                    request.AddJsonBody(new JArray(jsonBody));
                }
                else
                {
                    request.AddJsonBody(JsonConvert.DeserializeObject<JObject>(jsonBody));
                }
            }

            RestClient client = new RestClient(url);

            if (httpBasicAuthenticator)
            {
                client.Authenticator = new HttpBasicAuthenticator(Global.authenticatorUser, Global.authenticatorPassword);
            }

            if (ntlmAuthenticator)
            {
                client.Authenticator = new NtlmAuthenticator(Global.authenticatorUser, Global.authenticatorPassword);
            }

            client.AddHandler("application/json", new JsonDeserializer());

            return client.Execute<dynamic>(request);
        }

        public static XmlNodeList getElementXml(IRestResponse<dynamic> response, string elementTag)
        {
            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(response.Content);

            return responseXml.GetElementsByTagName(elementTag);
        }

        public static IRestResponse<dynamic> ExecuteSoapRequest(string url,
                                                            IDictionary<string, string> headers,
                                                            IDictionary<string, string> cookies,
                                                            string bodyXml,
                                                            bool httpBasicAuthenticator,
                                                            bool ntlmAuthenticator)
        {
            RestRequest request = new RestRequest(url, Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Xml;

            foreach (var header in headers)
            {
                request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }

            foreach (var cookie in cookies)
            {
                request.AddParameter(cookie.Key, cookie.Value, ParameterType.Cookie);
            }

            if (bodyXml != null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(bodyXml);
                request.AddParameter("text/xml", xmlDoc.OuterXml, ParameterType.RequestBody);
            }

            RestClient client = new RestClient(url);

            if (httpBasicAuthenticator)
            {
                client.Authenticator = new HttpBasicAuthenticator(Global.authenticatorUser, Global.authenticatorPassword);
            }

            if (ntlmAuthenticator)
            {
                client.Authenticator = new NtlmAuthenticator(Global.authenticatorUser, Global.authenticatorPassword);
            }

            return client.Execute<dynamic>(request);
        }
    }
}