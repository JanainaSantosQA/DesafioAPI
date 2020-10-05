using RestSharp;
using AutomacaoApiMantis.Bases;

namespace AutomacaoApiMantis.Requests.Filters
{
    public class GetAFilterRequest : RequestBase
    {
        public GetAFilterRequest(int? filterId)
        {
            requestService = "/api/rest/filters/" + filterId;
            method = Method.GET;
        }
    }
}