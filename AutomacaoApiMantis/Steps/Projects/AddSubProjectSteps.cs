using System;
using RestSharp;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Requests.Projects;

namespace AutomacaoApiMantis.Steps.Projects
{
    public class AddSubProjectSteps
    {
        public static ResponseDomain AdicionandoUmSubProjetoComSucesso(int projectParentId, int inheritParent, string nameSubProjetoChild, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            AddSubProjectRequest addSubProjectRequest = new AddSubProjectRequest(projectParentId);
            addSubProjectRequest.SetJsonBody(nameSubProjetoChild, inheritParent);
            IRestResponse<dynamic> response = addSubProjectRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.StatusDescription = response.StatusDescription.ToString();

            return responseDomain;
        }

        public static ResponseDomain AdicionandoUmSubProjetoSemSucesso(int projectParentId, int inheritParent, string nameSubProjetoChild, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            AddSubProjectRequest addSubProjectRequest = new AddSubProjectRequest(projectParentId);
            addSubProjectRequest.SetJsonBody(nameSubProjetoChild, inheritParent);
            IRestResponse<dynamic> response = addSubProjectRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.StatusDescription = response.StatusDescription.ToString();
            responseDomain.DataMessage = response.Data["message"].ToString();
            responseDomain.DataLocalized = response.Data["localized"].ToString();

            return responseDomain;
        }
    }
}