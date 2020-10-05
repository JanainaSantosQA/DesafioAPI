using RestSharp;
using System.IO;
using System.Text;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.Requests.Issues
{
    public class AttachATagToIssueRequest : RequestBase
    {
        public AttachATagToIssueRequest(string bugId)
        {
            requestService = "/api/rest/issues/" + bugId + "/notes";
            method = Method.POST;
            headers.Add("Content-Type", "application/json");
        }

        public void SetJsonBody(string noteText,
                                string viewState)

        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/AttachATagToIssueJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$noteText", noteText);
            jsonBody = jsonBody.Replace("$viewState", viewState);
        }
    }
}