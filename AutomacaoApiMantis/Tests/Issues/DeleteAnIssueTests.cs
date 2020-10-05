using RestSharp;
using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using System.Text.RegularExpressions;
using AutomacaoApiMantis.DBSteps.Issues;
using AutomacaoApiMantis.Requests.Issues;
using AutomacaoApiMantis.DBSteps.Projects;

namespace AutomacaoApiMantis.Tests.Issues
{
    [TestFixture]
    public class DeleteAnIssueTests : TestBase
    {
        ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();
        IssuesDBSteps issuesDBSteps = new IssuesDBSteps();

        [Test]
        public void DeletandoUmBug()
        {
            #region Inserindo um novo projeto
            string projectName = "DeletandoUmbug";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Inserindo um novo bug
            string summary = "DeletandoUmbugComSucesso";
            var bugCriadoDB = issuesDBSteps.InseriBugDB(projetoCriadoDB.ProjectId, summary);
            #endregion

            #region Parameters
            //Resultado Esperadp
            string statusCodeExpected = "NoContent";
            #endregion

            DeleteAnIssueRequest deleteAnIssueRequest = new DeleteAnIssueRequest(bugCriadoDB.BugId);
            IRestResponse<dynamic> response = deleteAnIssueRequest.ExecuteRequest();

            var consultaBugDB = issuesDBSteps.ConsultaBugDB(projetoCriadoDB.ProjectId, summary);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.IsNull(consultaBugDB, "O bug não foi excluído.");
            });

            projectsDBSteps.DeletaProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void DeletandoUmBugIdInexistente()
        {
            #region Parameters
            int bugId = 99999;

            //Resultado Esperadp
            string statusCodeExpected = "NotFound";
            #endregion

            DeleteAnIssueRequest deleteAnIssueRequest = new DeleteAnIssueRequest(bugId);
            IRestResponse<dynamic> response = deleteAnIssueRequest.ExecuteRequest();

            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");

            string[] arrayRegex = new string[]

            {
                "\"message\":(.*?)\"Issue (.*?)#(.*?)"+bugId+" not found\"",
                "\"localized\":(.*?)\"Issue "+bugId+" not found.\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }

        }

        [Test]
        public void DeletandoUmBugProjetoNaoExisteMais()
        {
            #region Inserindo um novo projeto
            string projectName = "DeletandoUmBugProjetoNaoExisteMais";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Inserindo um novo bug
            string summary = "DeletandoUmBugProjetoNaoExisteMais";
            var bugCriadoDB = issuesDBSteps.InseriBugDB(projetoCriadoDB.ProjectId, summary);
            #endregion

            #region Apagando o projeto criado anteriormente
            projectsDBSteps.DeletaProjetoDB(projetoCriadoDB.ProjectId);
            #endregion

            #region Parameters

            //Resultado Esperadp
            string statusCodeExpected = "NotFound";
            #endregion

            DeleteAnIssueRequest deleteAnIssueRequest = new DeleteAnIssueRequest(bugCriadoDB.BugId);
            IRestResponse<dynamic> response = deleteAnIssueRequest.ExecuteRequest();

            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");

            string[] arrayRegex = new string[]

            {
                "\"message\":(.*?)\"Project (.*?)#(.*?)"+projetoCriadoDB.ProjectId+" not found\"",
                "\"localized\":(.*?)\"Project (.*?)\\\""+projetoCriadoDB.ProjectId+"(.*?)\\\"(.*?) not found.\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }

            issuesDBSteps.DeletaBugDB(bugCriadoDB.BugId);
            issuesDBSteps.DeletaTextoBugDB(bugCriadoDB.BugId);
        }
    }
}