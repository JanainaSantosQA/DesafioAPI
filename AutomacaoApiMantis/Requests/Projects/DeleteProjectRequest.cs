using RestSharp;
using AutomacaoApiMantis.Bases;

namespace AutomacaoApiMantis.Requests.Projects
{
    public class DeleteProjectRequest : RequestBase
    {
        public DeleteProjectRequest(int projectId)
        {
            requestService = "/api/rest/projects/" + projectId;
            method = Method.DELETE;
        }
    }
}