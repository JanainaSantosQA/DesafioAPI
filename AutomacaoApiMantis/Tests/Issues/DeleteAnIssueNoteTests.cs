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
    public class DeleteAnIssueNoteTests : TestBase
    {
        IssuesDBSteps issuesDBSteps = new IssuesDBSteps();
        ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();

        [Test]
        public void DeletandoNotaBug()
        {
            #region Inserindo um novo projeto
            string projectName = "DeletandoNotaBug";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Inserindo um novo bug
            string summary = "DeletandoNotaBug";
            var bugCriadoDB = issuesDBSteps.InseriBugDB(projetoCriadoDB.ProjectId, summary);
            #endregion

            #region Inserindo uma nota
            string note = "DeletandoNotaBug";
            string bugNoteId = issuesDBSteps.InserirNotaBugDB(bugCriadoDB.BugId, note);
            #endregion

            #region Parameters
            //Resultado Esperadp
            string statusCodeExpected = "OK";
            #endregion

            DeleteAnIssueNoteRequest deleteAnIssueNoteRequest = new DeleteAnIssueNoteRequest(bugCriadoDB.BugId.ToString(), bugNoteId);
            IRestResponse<dynamic> response = deleteAnIssueNoteRequest.ExecuteRequest();

            var consultaNotaBugDB = issuesDBSteps.ConsultaNotaBugDB(bugCriadoDB.BugId, note);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.IsNull(consultaNotaBugDB, "O registro da nota não foi excluído.");
            });

            projectsDBSteps.DeletaProjetoDB(projetoCriadoDB.ProjectId);
            issuesDBSteps.DeletaBugDB(bugCriadoDB.BugId);
            issuesDBSteps.DeletaTextoBugDB(bugCriadoDB.BugId);
            issuesDBSteps.DeletaTextoNotaBugDB(bugNoteId, note);
        }

        [Test]
        public void DeletandoNotaBugIdNotaInexistente()
        {
            #region Inserindo um novo projeto
            string projectName = "DeletandoNotaBugIdNotaInexistente";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Inserindo um novo bug
            string summary = "DeletandoNotaBugIdNotaInexistente";
            var bugCriadoDB = issuesDBSteps.InseriBugDB(projetoCriadoDB.ProjectId, summary);
            #endregion

            #region Parameters
            string bugNoteId = "99999";

            //Resultado Esperadp
            string statusCodeExpected = "NotFound";
            #endregion

            DeleteAnIssueNoteRequest deleteAnIssueNoteRequest = new DeleteAnIssueNoteRequest(bugCriadoDB.BugId.ToString(), bugNoteId);
            IRestResponse<dynamic> response = deleteAnIssueNoteRequest.ExecuteRequest();

            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");

            string[] arrayRegex = new string[]

            {
                "\"message\":(.*?)\"Issue note (.*?)#"+bugNoteId+"(.*?) not found\"",
                "\"localized\":(.*?)\"Note not found.\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }

            projectsDBSteps.DeletaProjetoDB(projetoCriadoDB.ProjectId);
            issuesDBSteps.DeletaBugDB(bugCriadoDB.BugId);
            issuesDBSteps.DeletaTextoBugDB(bugCriadoDB.BugId);
        }

        [Test]
        public void DeletandoNotaBugIdNotaInvalido()
        {
            #region Inserindo um novo projeto
            string projectName = "DeletandoNotaBugIdNotaInvalido";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Inserindo um novo bug
            string summary = "DeletandoNotaBugIdNotaInvalido";
            var bugCriadoDB = issuesDBSteps.InseriBugDB(projetoCriadoDB.ProjectId, summary);
            #endregion

            #region Parameters
            string bugNoteId = "teste";

            //Resultado Esperadp
            string statusCodeExpected = "BadRequest";
            #endregion

            DeleteAnIssueNoteRequest deleteAnIssueNoteRequest = new DeleteAnIssueNoteRequest(bugCriadoDB.BugId.ToString(), bugNoteId);
            IRestResponse<dynamic> response = deleteAnIssueNoteRequest.ExecuteRequest();

            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");

            string[] arrayRegex = new string[]

            {
                "\"message\":(.*?)\"(.*?)'id'(.*?) must be (.*?)>=(.*?) 1\"",
                "\"localized\":(.*?)\"Invalid value for (.*?)'id'(.*?)\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }

            projectsDBSteps.DeletaProjetoDB(projetoCriadoDB.ProjectId);
            issuesDBSteps.DeletaBugDB(bugCriadoDB.BugId);
            issuesDBSteps.DeletaTextoBugDB(bugCriadoDB.BugId);
        }

        [Test]
        public void DeletandoNotaIdBugIncorreto()
        {
            #region Inserindo um novo projeto
            string projectName = "DeletandoNotaBugIdNotaIncorreto";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Inserindo um novo bug
            string summary = "DeletandoNotaBugIdNotaIncorreto";
            var bugCriadoDB = issuesDBSteps.InseriBugDB(projetoCriadoDB.ProjectId, summary);
            #endregion

            #region Inserindo uma nota
            string note = "DeletandoNotaIdBugIncorreto";
            string bugNoteId = issuesDBSteps.InserirNotaBugDB(bugCriadoDB.BugId, note);
            #endregion

            #region Parameters
            string bugId = "999999";

            //Resultado Esperadp
            string statusCodeExpected = "BadRequest";
            #endregion

            DeleteAnIssueNoteRequest deleteAnIssueNoteRequest = new DeleteAnIssueNoteRequest(bugId, bugNoteId);
            IRestResponse<dynamic> response = deleteAnIssueNoteRequest.ExecuteRequest();

            var consultaNotaBugDB = issuesDBSteps.ConsultaNotaBugDB(bugCriadoDB.BugId, note);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.IsNotNull(consultaNotaBugDB, "O registro da nota foi excluído.");
            });

            string[] arrayRegex = new string[]

            {
                "\"message\":(.*?)\"Issue note doesn't belong to issue\"",
                "\"localized\":(.*?)\"Invalid value for 'id'\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }

            projectsDBSteps.DeletaProjetoDB(projetoCriadoDB.ProjectId);
            issuesDBSteps.DeletaBugDB(bugCriadoDB.BugId);
            issuesDBSteps.DeletaTextoBugDB(bugCriadoDB.BugId);
            issuesDBSteps.DeletaTextoNotaBugDB(bugNoteId, note);
        }
    }
}