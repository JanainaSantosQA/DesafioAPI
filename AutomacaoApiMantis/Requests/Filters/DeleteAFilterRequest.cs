using RestSharp;
using AutomacaoApiMantis.Bases;

namespace AutomacaoApiMantis.Requests.Filters
{
    public class DeleteAFilterRequest : RequestBase
    {
        public DeleteAFilterRequest(int? filterId)
        {
            requestService = "/api/rest/filters/" + filterId;
            method = Method.DELETE;
        }
    }
}