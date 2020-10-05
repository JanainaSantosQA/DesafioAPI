using RestSharp;
using AutomacaoApiMantis.Bases;

namespace AutomacaoApiMantis.Requests.Users
{
    public class DeleteUserRequest : RequestBase
    {
        public DeleteUserRequest(int? userId)
        {
            requestService = "/api/rest/users/" + userId;
            method = Method.DELETE;
        }
    }
}