using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Steps.Projects;
using AutomacaoApiMantis.DBSteps.Projects;

namespace AutomacaoApiMantis.Tests.Projects
{
    [TestFixture]
    public class CreateProjectTests : TestBase
    {
        public ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();

        [Test]
        public void CriandoProjeto()
        {
            #region Parameters
            int projectId = 0;
            string projectName = "AutomacaoAPI_Mantis";
            int projectStatusId = 10;
            string statusName = "testing";
            string statusLabel = "testing";
            string description = "Criando um novo projeto.";
            int enabled = 1;
            string filePath = "/tmp/";
            int viewStateId = 10;
            string viewStateName = "public";
            string viewStateLabel = "public";

            //Resultado esperado
            string statusCodeExpected = "Created";
            #endregion

            var response = CreateProjectSteps.CriandoProjetoComSucesso(projectId, projectName, projectStatusId, statusName, statusLabel, description, enabled, filePath, viewStateId, viewStateName, viewStateLabel, statusCodeExpected);
            var consultaProjectDB = projectsDBSteps.ConsultaProjetoDB(response.ProjectId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(consultaProjectDB.ProjectName, response.ProjectName, projectName, "O nome do projeto não é o esperado.");
                Assert.AreEqual(consultaProjectDB.ProjectStatusId, response.ProjectStatusId, projectStatusId, "O status não é o esperado.");
                Assert.AreEqual(consultaProjectDB.Enabled, response.Enabled, enabled, "A ativação não é a esperada.");
                Assert.AreEqual(consultaProjectDB.ViewState, response.ViewState, viewStateId, "A visualização do estado não é a esperada.");
            });

            projectsDBSteps.DeletaProjetoDB(response.ProjectId);
        }
    }
}