using RestSharp;
using System.IO;
using System.Text;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.Requests.Projects
{
    public class UpdateProjectRequest : RequestBase
    {
        public UpdateProjectRequest(int projectId)
        {
            requestService = "/api/rest/projects/" + projectId;
            method = Method.PATCH;
            headers.Add("Content-Type", "application/json");
        }
        public void SetJsonBody(int projectId,
                                string projectName,
                                int enabled)

        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projects/UpdateProjectJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$projectId", projectId.ToString())
                               .Replace("$projectName", projectName)
                               .Replace("$enabled", enabled.ToString());
        }
    }
}