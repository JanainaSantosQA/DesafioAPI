using RestSharp;
using System.IO;
using System.Text;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.Requests.Projects
{
    public class CreateProjectRequest : RequestBase
    {
        public CreateProjectRequest()
        {
            requestService = "/api/rest/projects/";
            method = Method.POST;
            headers.Add("Content-Type", "application/json");
        }

        public void SetJsonBody(int projectId,
                                string projectName,
                                int projectStatusId,
                                string statusName,
                                string statusLabel,
                                string description,
                                int enabled,
                                string filePath,
                                int viewStateId,
                                string viewStateName,
                                string viewStateLabel)

        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projects/CreateProjectJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$projectId", projectId.ToString())
                               .Replace("$projectName", projectName)
                               .Replace("$projectStatusId", projectStatusId.ToString())
                               .Replace("$statusName", statusName)
                               .Replace("$statusLabel", statusLabel)
                               .Replace("$description", description)
                               .Replace("$enabled", enabled.ToString())
                               .Replace("$filePath", filePath)
                               .Replace("$viewStateId", viewStateId.ToString())
                               .Replace("$viewStateName", viewStateName)
                               .Replace("$viewStateLabel", viewStateLabel);
        }
    }
}