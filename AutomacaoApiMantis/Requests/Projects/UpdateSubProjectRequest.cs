using System.IO;
using RestSharp;
using System.Text;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.Requests.Projects
{
    public class UpdateSubProjectRequest : RequestBase
    {
        public UpdateSubProjectRequest(int projectParentId, int projectChildId)
        {
            requestService = "/api/rest/projects/" + projectParentId + "/subprojects/" + projectChildId + "";
            method = Method.PATCH;
            headers.Add("Content-Type", "application/json");
        }

        public void SetJsonBody(string projectName,
                                int inheritParent)

        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projects/UpdateSubProjectJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$projectName", projectName)
                               .Replace("$inheritParent", inheritParent.ToString());
        }
    }
}