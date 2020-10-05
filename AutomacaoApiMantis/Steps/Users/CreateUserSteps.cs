using System;
using RestSharp;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Requests.Users;

namespace AutomacaoApiMantis.Steps.Users
{
    public class CreateUserSteps
    {
        public static UserDomain CriandoUsuarioComSucesso(string username, string password, string realName, string email, string acessLevelName, string ativacao, string protecao, string statusCodeExpected)
        {
            UserDomain userDomain = new UserDomain();

            CreateUserRequest createUserRequest = new CreateUserRequest();
            createUserRequest.SetJsonBody(username, password, realName, email, acessLevelName, ativacao, protecao);
            IRestResponse<dynamic> response = createUserRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            userDomain.Username = response.Data["user"]["name"].ToString();
            userDomain.RealName = response.Data["user"]["real_name"].ToString();
            userDomain.Email = response.Data["user"]["email"].ToString();

            return userDomain;

        }
        public static ResponseDomain CriandoUsuarioSemSucesso(string username, string password, string realName, string email, string acessLevelName, string enabled, string protecao, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            CreateUserRequest createUserRequest = new CreateUserRequest();
            createUserRequest.SetJsonBody(username, password, realName, email, acessLevelName, enabled, protecao);
            IRestResponse<dynamic> response = createUserRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.DataMessage = response.Data["message"].ToString();
            responseDomain.DataLocalized = response.Data["localized"].ToString();

            return responseDomain;
        }
    }
}