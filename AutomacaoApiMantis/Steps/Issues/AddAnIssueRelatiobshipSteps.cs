using System;
using RestSharp;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Requests.Issues;

namespace AutomacaoApiMantis.Steps.Issues
{
    public class AddAnIssueRelatiobshipSteps
    {
        public static ResponseDomain AdicionandoRelacionamentoBug(string bugId, string typeName, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            AddAnIssueRelatiobshipRequest addAnIssueRelatiobshipRequest = new AddAnIssueRelatiobshipRequest(bugId);
            addAnIssueRelatiobshipRequest.SetJsonBody(bugId, typeName);
            IRestResponse<dynamic> response = addAnIssueRelatiobshipRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.StatusDescription = response.StatusDescription.ToString();
            responseDomain.Content = response.Content.ToString();

            return responseDomain;
        }
    }
}