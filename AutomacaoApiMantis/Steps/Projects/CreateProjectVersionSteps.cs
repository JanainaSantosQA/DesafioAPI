using System;
using RestSharp;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Requests.Projects;

namespace AutomacaoApiMantis.Steps.Projects
{
    public class CreateProjectVersionSteps
    {
        public static string CriandoVersaoProjetoComSucesso(string versionName, string versionDescription, int versionReleased, int versionObsolete, string timestamp, string projectId, string statusCodeExpected)
        {

            CreateProjectVersionRequest createProjectVersionRequest = new CreateProjectVersionRequest(projectId);
            createProjectVersionRequest.SetJsonBody(versionName, versionDescription, versionReleased, versionObsolete, timestamp);
            IRestResponse<dynamic> response = createProjectVersionRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            return String.Join("", System.Text.RegularExpressions.Regex.Split(response.StatusDescription, @"[^\d]"));

        }

        public static ResponseDomain CriandoVersaoProjetoSemSucesso(string versionName, string versionDescription, int versionReleased, int versionObsolete, string timestamp, string projectId, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            CreateProjectVersionRequest createProjectVersionRequest = new CreateProjectVersionRequest(projectId);
            createProjectVersionRequest.SetJsonBody(versionName, versionDescription, versionReleased, versionObsolete, timestamp);
            IRestResponse<dynamic> response = createProjectVersionRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.DataMessage = response.Data["message"].ToString();

            return responseDomain;

        }
    }
}