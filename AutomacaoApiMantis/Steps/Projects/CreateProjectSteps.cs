using System;
using RestSharp;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Requests.Projects;

namespace AutomacaoApiMantis.Steps.Projects
{
    public class CreateProjectSteps
    {
        public static ProjectDomain CriandoProjetoComSucesso(int projectId, string projectName, int projectStatusId, string statusName, string statusLabel, string description, int enabled, string filePath, int viewStateId, string viewStateName, string viewStateLabel, string statusCodeExpected)
        {
            ProjectDomain projectDomain = new ProjectDomain();

            CreateProjectRequest createProjectRequest = new CreateProjectRequest();
            createProjectRequest.SetJsonBody(projectId, projectName, projectStatusId, statusName, statusLabel, description, enabled, filePath, viewStateId, viewStateName, viewStateLabel);
            IRestResponse<dynamic> response = createProjectRequest.ExecuteRequest();
            if (!response.StatusCode.ToString().Equals(statusCodeExpected)) { throw new Exception("Erro ao executar a requisição."); }

            projectDomain.ProjectId = Convert.ToInt32(response.Data["project"]["id"]);
            projectDomain.ProjectName = response.Data["project"]["name"].ToString();
            projectDomain.ProjectStatusId = Convert.ToInt32((response.Data["project"]["status"]["id"]));
            projectDomain.Enabled = Convert.ToInt32((response.Data["project"]["enabled"]));
            projectDomain.ViewState = Convert.ToInt32((response.Data["project"]["view_state"]["id"]));

            return projectDomain;
        }
    }
}