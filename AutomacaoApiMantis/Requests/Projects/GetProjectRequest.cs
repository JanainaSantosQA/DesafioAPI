using RestSharp;
using AutomacaoApiMantis.Bases;

namespace AutomacaoApiMantis.Requests.Projects
{
    public class GetProjectRequest : RequestBase
    {
        public GetProjectRequest(int projectId)
        {
            requestService = "/api/rest/projects/" + projectId;
            method = Method.GET;
        }
    }
}