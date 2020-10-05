using System.IO;
using RestSharp;
using System.Text;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.Requests.Issues
{
    public class AddAnIssueRelatiobshipRequest : RequestBase
    {

        public AddAnIssueRelatiobshipRequest(string bugId)
        {
            requestService = "/api/rest/issues/" + bugId + "/relationships/";
            method = Method.POST;
            headers.Add("Content-Type", "application/json");
        }

        public void SetJsonBody(string bugId,
                                string typeName)

        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/AddAnIssueRelatiobshipJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$bugId", bugId);
            jsonBody = jsonBody.Replace("$typeName", typeName);
        }
    }
}