using System;
using RestSharp;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Requests.Issues;

namespace AutomacaoApiMantis.Steps.Issues
{
    public class CreateAnIssueSteps
    {
        public static ResponseDomain CriandoUmBug(string summary, string description, string additionalInformation, int projectId, string projectName, string tagName, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            CreateAnIssueRequest createAnIssueRequest = new CreateAnIssueRequest();
            createAnIssueRequest.SetJsonBody(summary, description, additionalInformation, projectId, projectName, tagName);
            IRestResponse<dynamic> response = createAnIssueRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.Content = response.Content.ToString();

            return responseDomain;
        }
    }
}