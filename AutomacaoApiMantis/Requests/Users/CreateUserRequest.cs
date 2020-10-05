using RestSharp;
using System.IO;
using System.Text;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.Requests.Users
{
    public class CreateUserRequest : RequestBase
    {
        public CreateUserRequest()
        {
            requestService = "/api/rest/users/";
            method = Method.POST;
            headers.Add("Content-Type", "application/json");
        }

        public void SetJsonBody(string username,
                                string password,
                                string realName,
                                string email,
                                string acessLevelName,
                                string enabled,
                                string protecao)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Users/CreateUserJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$username", username)
                               .Replace("$password", password)
                               .Replace("$realName", realName)
                               .Replace("$email", email)
                               .Replace("$acessLevelName", acessLevelName)
                               .Replace("$enabled", enabled)
                               .Replace("$protected", protecao);
        }
    }
}