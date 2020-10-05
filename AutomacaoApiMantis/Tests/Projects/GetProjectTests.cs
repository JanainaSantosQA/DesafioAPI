using System;
using RestSharp;
using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.DBSteps.Projects;
using AutomacaoApiMantis.Requests.Projects;

namespace AutomacaoApiMantis.Tests.Projects
{
    [TestFixture]
    public class GetProjectTests : TestBase
    {
        public ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();

        [Test]
        public void ConsultandoUmProjeto()
        {
            #region Inserindo um novo projeto
            string projectName = "ConsultandoUmProjeto";
            var consultaProjetoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Parameters

            //Resultado Esperado
            string statusCodeExpected = "OK";
            #endregion

            GetProjectRequest getProjectRequest = new GetProjectRequest(consultaProjetoDB.ProjectId);
            IRestResponse<dynamic> response = getProjectRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O status code retornado não é o esperado.");
                Assert.AreEqual(consultaProjetoDB.ProjectName, response.Data["projects"][0]["name"].ToString(), "O nome do projeto está diferente.");
                Assert.AreEqual(consultaProjetoDB.ProjectStatusId, Convert.ToInt32(response.Data["projects"][0]["status"]["id"]), "O id do projeto está diferente.");
                Assert.AreEqual(consultaProjetoDB.Enabled, Convert.ToInt32(response.Data["projects"][0]["enabled"]), "O status da ativação está diferente.");
                Assert.AreEqual(consultaProjetoDB.ViewState, Convert.ToInt32(response.Data["projects"][0]["view_state"]["id"]), "O view state está diferente.");
                Assert.AreEqual(consultaProjetoDB.Description, response.Data["projects"][0]["description"].ToString(), "A descrição está diferente.");
            });

            projectsDBSteps.DeletaProjetoDB(consultaProjetoDB.ProjectId);
        }

        [Test]
        public void RetornaProjetoCriadoIdInexistente()
        {
            #region Parameters
            int projectId = 99999;

            //Resultado Esperado
            string statusCodeExpected = "NotFound";
            string statusDescriptionExpected = "Project #" + projectId + " not found";
            #endregion

            GetProjectRequest getProjectRequest = new GetProjectRequest(projectId);
            IRestResponse<dynamic> response = getProjectRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O status code retornado não é o esperado.");
                Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O status description retornado não é o esperado.");
            });
        }
    }
}