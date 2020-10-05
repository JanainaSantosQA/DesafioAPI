using RestSharp;
using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.DBSteps.Projects;
using AutomacaoApiMantis.Requests.Projects;

namespace AutomacaoApiMantis.Tests.Projects
{
    [TestFixture]
    public class DeleteProjectTests : TestBase
    {
        ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();

        [Test]
        public void DeletaProjetoComSucesso()
        {
            #region Inserindo um novo projeto
            string projectName = "DeletaProjetoComSucesso";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Parameters
            //Resultado Esperado
            string statusCodeExpected = "OK";
            string statusDescriptionExpected = "Project with id " + projetoCriadoDB.ProjectId + " deleted.";
            #endregion

            DeleteProjectRequest deleteProjectRequest = new DeleteProjectRequest(projetoCriadoDB.ProjectId);
            IRestResponse<dynamic> response = deleteProjectRequest.ExecuteRequest();

            var consultaProjetoDB = projectsDBSteps.ConsultaProjetoDB(projetoCriadoDB.ProjectId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode não é o esperado.");
                Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O StatusDescription não é o esperado.");
                Assert.IsNull(consultaProjetoDB, "Um registro foi encontrado no banco de dados.");
            });

        }

        [Test]
        public void DeletaProjetoIdInexistente()
        {
            #region Parameters
            int projectId = 0;

            //Resultado Esperado
            string statusCodeExpected = "BadRequest";
            string statusDescriptionExpected = "Invalid project id.";
            #endregion

            DeleteProjectRequest deleteProjectRequest = new DeleteProjectRequest(projectId);
            IRestResponse<dynamic> response = deleteProjectRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode não é o esperado.");
                Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O StatusDescription não é o esperado.");
            });
        }
    }
}