using System;
using RestSharp;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Requests.Projects;

namespace AutomacaoApiMantis.Steps.Projects
{
    public class UpdateProjectSteps
    {
        public static ProjectDomain AtualizandoUmProjetoComSucesso(int projectId, string projectNewName, int projectEnabled, string statusCodeExpected)
        {
            ProjectDomain projectDomain = new ProjectDomain();

            UpdateProjectRequest updateProjectRequest = new UpdateProjectRequest(projectId);
            updateProjectRequest.SetJsonBody(projectId, projectNewName, projectEnabled);
            IRestResponse<dynamic> response = updateProjectRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            projectDomain.ProjectName = response.Data["project"]["name"].ToString();
            projectDomain.Enabled = Convert.ToInt32(response.Data["project"]["status"]["enabled"]);

            return projectDomain;
        }

        public static ResponseDomain AtualizandoUmProjetoSemSucesso(int projectId, string projectNewName, int projectEnabled, string statusCodeExpected)
        {
            ResponseDomain responseDomain = new ResponseDomain();

            UpdateProjectRequest updateProjectRequest = new UpdateProjectRequest(projectId);
            updateProjectRequest.SetJsonBody(projectId, projectNewName, projectEnabled);
            IRestResponse<dynamic> response = updateProjectRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            responseDomain.StatusDescription = response.StatusDescription.ToString();

            return responseDomain;
        }
    }
}