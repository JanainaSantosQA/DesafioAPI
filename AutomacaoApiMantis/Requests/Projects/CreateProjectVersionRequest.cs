using RestSharp;
using System.IO;
using System.Text;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.Requests.Projects
{
    public class CreateProjectVersionRequest : RequestBase
    {
        public CreateProjectVersionRequest(string projectId)
        {
            requestService = "/api/rest/projects/" + projectId + "/versions/";
            method = Method.POST;
            headers.Add("Content-Type", "application/json");
        }

        public void SetJsonBody(string versionName,
                                string versionDescription,
                                int versionReleased,
                                int versionObsolete,
                                string timestamp)

        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projects/CreateProjectVersionJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$versionName", versionName)
                               .Replace("$versionDescription", versionDescription)
                               .Replace("$versionReleased", versionReleased.ToString())
                               .Replace("$versionObsolete", versionObsolete.ToString())
                               .Replace("$timestamp", timestamp);

        }
    }
}