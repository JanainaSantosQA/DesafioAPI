using System;
using RestSharp;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Requests.Issues;

namespace AutomacaoApiMantis.Steps.Issues
{
    public class AttachATagToIssueSteps
    {
        public static ResponseDomain InserindoUmaNotaNoBug (string bugId, string noteText, string viewState, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            AttachATagToIssueRequest attachATagToIssueRequest = new AttachATagToIssueRequest(bugId);
            attachATagToIssueRequest.SetJsonBody(noteText, viewState);
            IRestResponse<dynamic> response = attachATagToIssueRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.StatusDescription = response.StatusDescription.ToString();
            responseDomain.Content = response.Content.ToString();

            return responseDomain;
        }
    }
}