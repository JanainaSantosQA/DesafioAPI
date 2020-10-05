using RestSharp;
using AutomacaoApiMantis.Bases;

namespace AutomacaoApiMantis.Requests.Issues
{
    public class DeleteAnIssueNoteRequest : RequestBase
    {
        public DeleteAnIssueNoteRequest(string bugId, string bugNoteId)
        {
            requestService = "/api/rest/issues/" + bugId + "/notes/" + bugNoteId + "";
            method = Method.DELETE;
            headers.Add("Content-Type", "application/json");
        }
    }
}