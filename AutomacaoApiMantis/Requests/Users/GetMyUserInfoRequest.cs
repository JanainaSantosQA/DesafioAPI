using RestSharp;
using AutomacaoApiMantis.Bases;

namespace AutomacaoApiMantis.Requests.Users
{
    public class GetMyUserInfoRequest : RequestBase
    {
        public GetMyUserInfoRequest()
        {
            requestService = "/api/rest/users/me";
            method = Method.GET;
        }
    }
}