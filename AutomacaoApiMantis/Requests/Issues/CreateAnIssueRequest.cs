using RestSharp;
using System.IO;
using System.Text;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.Requests.Issues
{
    public class CreateAnIssueRequest : RequestBase
    {
        public CreateAnIssueRequest()
        {
            requestService = "/api/rest/issues";
            method = Method.POST;
            headers.Add("Content-Type", "application/json");
        }

        public void SetJsonBody(string summary,
                                string description,
                                string additionalInformation,
                                int projectId,
                                string projectName,
                                string tagName)

        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/CreateAnIssueJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$summary", summary);
            jsonBody = jsonBody.Replace("$description", description);
            jsonBody = jsonBody.Replace("$additionalInformation", additionalInformation);
            jsonBody = jsonBody.Replace("$projectId", projectId.ToString());
            jsonBody = jsonBody.Replace("$projectName", projectName);
            jsonBody = jsonBody.Replace("$tagName", tagName);
        }
    }
}