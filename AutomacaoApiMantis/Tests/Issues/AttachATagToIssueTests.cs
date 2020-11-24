using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using System.Text.RegularExpressions;
using AutomacaoApiMantis.Steps.Issues;
using AutomacaoApiMantis.DBSteps.Issues;
using AutomacaoApiMantis.DBSteps.Projects;

namespace AutomacaoApiMantis.Tests.Issues
{
    [TestFixture]
    public class AttachATagToIssueTests : TestBase
    {
        ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();
        IssuesDBSteps issuesDBSteps = new IssuesDBSteps();

        [Test]
        public void InserindoUmaNotaNoBug()
        {
            #region Inserindo um novo projeto
            string projectName = "InserindoUmaNotaNoBug";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Inserindo um novo bug
            string summary = "InserindoUmaNotaNoBug";
            var bugCriadoDB = issuesDBSteps.InseriBugDB(projetoCriadoDB.ProjectId, summary);
            #endregion

            #region Parameters
            string noteText = "InserindoUmaNotaNoBug";
            string viewState = "public";

            //Resultado Esperado
            string statusCodeExpected = "Created";
            string viewStateExpected = "10";
            string statusDescriptionExpected = "Issue Note Created with id " + bugCriadoDB.BugId + "";
            #endregion

            var response = AttachATagToIssueSteps.InserindoUmaNotaNoBug(bugCriadoDB.BugId.ToString(), noteText, viewState, statusCodeExpected);

            var consultaNotaBugDB = issuesDBSteps.ConsultaNotaBugDB(bugCriadoDB.BugId, noteText);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusDescriptionExpected, response.StatusDescription.ToString(), "O StatusDescription retornado não é o esperado.");
                Assert.AreEqual(noteText, consultaNotaBugDB[10], "O texto da nota registrado não é o esperado.");
                Assert.AreEqual(viewStateExpected, consultaNotaBugDB[4], "O view state registrado não é o esperado.");
            });

            projectsDBSteps.DeletaProjetoDB(projetoCriadoDB.ProjectId);
            issuesDBSteps.DeletaBugDB(bugCriadoDB.BugId);
            issuesDBSteps.DeletaTextoBugDB(bugCriadoDB.BugId);
            issuesDBSteps.DeletaTextoNotaBugDB(consultaNotaBugDB[0], noteText);
            issuesDBSteps.DeletaNotaBugDB(bugCriadoDB.BugId, consultaNotaBugDB[0]);
        }

        [Test]
        public void InserindoUmaNotaNoBugIdBugInvalido()
        {
            #region Parameters
            string bugId = "test";
            string noteText = "InserindoUmaNotaNoBugIdBugInvalido";
            string viewState = "public";

            //Resultado Esperado
            string statusCodeExpected = "BadRequest";
            #endregion

            var response = AttachATagToIssueSteps.InserindoUmaNotaNoBug(bugId, noteText, viewState, statusCodeExpected);

            string[] arrayRegex = new string[]
            {
                "\"message\":\"(.*?)'issue_id'(.*?) must be numeric\"",
                "localized\":\"Invalid value for (.*?)'issue_id'(.*?)\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }
        }

        [Test]
        public void InserindoUmaNotaNoBugIdBugInexistente()
        {
            #region Parameters
            string bugId = "99999";
            string noteText = "InserindoUmaNotaNoBugIdBugInexistente";
            string viewState = "public";

            //Resultado Esperado
            string statusCodeExpected = "NotFound";
            #endregion

            var response = AttachATagToIssueSteps.InserindoUmaNotaNoBug(bugId, noteText, viewState, statusCodeExpected);

            string[] arrayRegex = new string[]
            {
                "\"message\":\"Issue (.*?)#"+bugId+"(.*?) not found\"",
                "localized\":\"Issue "+bugId+" not found.\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }
        }

        [Test]
        public void InserindoUmaNotaNoBugSemInformarOTexto()
        {
            #region Inserindo um novo projeto
            string projectName = "InserindoUmaNotaNoBugSemInformarOTexto";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Inserindo um novo bug
            string summary = "InserindoUmaNotaNoBugSemInformarOTexto";
            var bugCriadoDB = issuesDBSteps.InseriBugDB(projetoCriadoDB.ProjectId, summary);
            #endregion

            #region Parameters

            //Resultado Esperado
            string statusCodeExpected = "BadRequest";
            string noteText = "";
            string viewState = "public";
            #endregion

            var response = AttachATagToIssueSteps.InserindoUmaNotaNoBug(bugCriadoDB.BugId.ToString(), noteText, viewState, statusCodeExpected);

            var consultaNotaBugDB = issuesDBSteps.ConsultaNotaBugDB(bugCriadoDB.BugId, noteText);

           Assert.IsNull(consultaNotaBugDB, "Um registro de nota foi encontrado no banco.");       

            string[] arrayRegex = new string[]
            {
                "\"message\":(.*?)\"Issue note not specified.\"",
                "\"localized\":(.*?)\"A necessary field (.*?)\\\"Note(.*?)\\\"(.*?) was empty. Please recheck your inputs.\"",
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
    }
}