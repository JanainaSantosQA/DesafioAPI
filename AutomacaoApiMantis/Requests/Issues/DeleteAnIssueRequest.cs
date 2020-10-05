using RestSharp;
using AutomacaoApiMantis.Bases;

namespace AutomacaoApiMantis.Requests.Issues
{
    public class DeleteAnIssueRequest : RequestBase
    {
        public DeleteAnIssueRequest(int issueId)
        {
            requestService = "/api/rest/issues/" + issueId + "";
            method = Method.DELETE;
        }
    }
}