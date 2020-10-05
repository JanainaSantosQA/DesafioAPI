using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Steps.Projects;
using AutomacaoApiMantis.DBSteps.Projects;

namespace AutomacaoApiMantis.Tests.Projects
{
    [TestFixture]
    public class UpdateProjectTests : TestBase
    {
        ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();

        [Test]
        public void AtualizandoProjetoDadosValidos()
        {
            #region Inserindo novo projeto
            string projectName = "AtualizaProjetoDadosValidos";
            var inseriProjetoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Parameters
            int projectId = inseriProjetoDB.ProjectId;
            string projectNewName = "AtualizandoNomeProjeto";
            int projectEnabled = 0;

            //Resultado Esperado
            string statusCodeExpected = "OK";
            string statusDescriptionExpected = "Project with id " + projectId + " Updated";
            #endregion

            var response = UpdateProjectSteps.AtualizandoUmProjetoComSucesso(projectId, projectNewName, projectEnabled, statusCodeExpected);
            var consultaProjectDB = projectsDBSteps.ConsultaProjetoDB(projectId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(consultaProjectDB.ProjectName, response.ProjectName, "O nome do projeto não foi atualizado com sucesso!");
                Assert.AreEqual(consultaProjectDB.Enabled, response.Enabled, "A ativação do projeto não foi atualizada com sucesso!");
            });

            projectsDBSteps.DeletaProjetoDB(projectId);
        }

        [Test]
        public void AtualizandoProjetoIdInexistente()
        {
            #region Parameters
            int projectId = 0;
            string projectNewName = "UpdateIDInexistente";
            int projectEnabled = 1;

            //Resultado esperado
            string statusCodeExpected = "BadRequest";
            string statusDescriptionExpected = "Invalid project id.";
            #endregion

            var response = UpdateProjectSteps.AtualizandoUmProjetoSemSucesso(projectId, projectNewName, projectEnabled, statusCodeExpected);

            Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O StatusDescription não é o esperado.");

        }
    }
}