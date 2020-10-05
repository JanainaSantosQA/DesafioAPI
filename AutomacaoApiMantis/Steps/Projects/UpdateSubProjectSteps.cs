using System;
using RestSharp;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Requests.Projects;

namespace AutomacaoApiMantis.Steps.Projects
{
    public class UpdateSubProjectSteps
    {
        public static ResponseDomain AtualizandoUmSubProjetoComSucesso(int projectParentId, int projectChildId, string nameSubProjetoChild, int inheritParent, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            UpdateSubProjectRequest updateSubProjectRequest = new UpdateSubProjectRequest(projectParentId, projectChildId);
            updateSubProjectRequest.SetJsonBody(nameSubProjetoChild, inheritParent);
            IRestResponse<dynamic> response = updateSubProjectRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.StatusDescription = response.StatusDescription.ToString();

            return responseDomain;
        }

        public static ResponseDomain AtualizandoUmSubProjetoSemSucesso(int projectParentId, int projectChildId, string nameSubProjetoChild, int inheritParent, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            UpdateSubProjectRequest updateSubProjectRequest = new UpdateSubProjectRequest(projectParentId, projectChildId);
            updateSubProjectRequest.SetJsonBody(nameSubProjetoChild, inheritParent);
            IRestResponse<dynamic> response = updateSubProjectRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.StatusDescription = response.StatusDescription.ToString();
            responseDomain.DataMessage = response.Data["message"].ToString();

            return responseDomain;
        }
    }
}