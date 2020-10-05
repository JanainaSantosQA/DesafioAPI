using RestSharp;
using System.IO;
using System.Text;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.Requests.Projects
{
    public class AddSubProjectRequest : RequestBase
    {
        public AddSubProjectRequest(int projectParentId)
        {
            requestService = "/api/rest/projects/" + projectParentId + "/subprojects";
            method = Method.POST;
            headers.Add("Content-Type", "application/json");
        }

        public void SetJsonBody(string subProjetoChildName,
                                int inheritParent)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projects/AddSubProjectJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$nameSubProjetoChild", subProjetoChildName)
                               .Replace("$inheritParent", inheritParent.ToString());

        }
    }
}