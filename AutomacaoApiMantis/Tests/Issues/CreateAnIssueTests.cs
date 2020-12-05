using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using System.Text.RegularExpressions;
using AutomacaoApiMantis.Steps.Issues;
using AutomacaoApiMantis.DBSteps.Issues;
using AutomacaoApiMantis.DBSteps.Projects;

namespace AutomacaoApiMantis.Tests.Issues
{
    [TestFixture]
    public class CreateAnIssueTests : TestBase
    {
        IssuesDBSteps issuesDBSteps = new IssuesDBSteps();
        ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();

        [Test]
        public void CriandoUmBug()
        {
            #region Inserindo um novo projeto
            string projectName = "CriandoUmBug";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Parameters
            string summary = "CriandoUmBug";
            string description = "Description for sample REST issue.";
            string additionalInformation = "More info about the issue";
            string tagName = "mantishub";

            //Resultado Esperadp
            string statusCodeExpected = "OK";
            #endregion

            CreateAnIssueSteps.CriandoUmBug(summary, description, additionalInformation, projetoCriadoDB.ProjectId, projetoCriadoDB.ProjectName, tagName, statusCodeExpected);

            var bugRegistradoDB = issuesDBSteps.ConsultaBugDB(projetoCriadoDB.ProjectId, summary);

            Assert.IsNotNull(bugRegistradoDB, "O bug não foi cadastrado.");

            projectsDBSteps.DeletaProjetoDB(projetoCriadoDB.ProjectId);
            issuesDBSteps.DeletaBugDB(bugRegistradoDB.BugId);
            issuesDBSteps.DeletaTextoBugDB(bugRegistradoDB.BugId);
            issuesDBSteps.DeletaTagBugDB(bugRegistradoDB.BugId);
            issuesDBSteps.DeletaHistoricoBugDB(bugRegistradoDB.BugId);
        }

        [Test]
        public void CriandoUmBugInformandoProjetoInexistente()
        {
            #region Parameters
            string summary = "CriandoUmBugInformandoProjetoInexistente";
            string description = "Description for sample REST issue.";
            string additionalInformation = "More info about the issue";
            string tagName = "mantishub";
            int projectId = 999999;
            string projectName = "CriandoUmBugInformandoProjetoInexistente";

            //Resultado Esperadp
            string statusCodeExpected = "NotFound";
            #endregion

            var response = CreateAnIssueSteps.CriandoUmBug(summary, description, additionalInformation, projectId, projectName, tagName, statusCodeExpected);

            var bugRegistradoDB = issuesDBSteps.ConsultaBugDB(projectId, summary);

            Assert.IsNull(bugRegistradoDB, "O projeto informado não existe, porém um registro de bug foi retornado.");

            string[] arrayRegex = new string[]
            {
            "\"message\":(.*?)\"Project (.*?)'"+projectId+"'(.*?) not found\"",
            "\"localized\":(.*?)\"Project (.*?)\\\""+projectId+"(.*?)\\\"(.*?) not found.\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }
        }

        [Test]
        public void CriandoUmBugSumarioEmBranco()
        {
            #region Inserindo um novo projeto
            string projectName = "CriandoUmBugSumarioEmBranco";
            var projetoCriado = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Parameters
            string summary = "";
            string description = "Description for sample REST issue.";
            string additionalInformation = "More info about the issue";
            string tagName = "mantishub";

            //Resultado Esperadp
            string statusCodeExpected = "BadRequest";
            #endregion

            var response = CreateAnIssueSteps.CriandoUmBug(summary, description, additionalInformation, projetoCriado.ProjectId, projectName, tagName, statusCodeExpected);

            var bugRegistradoDB = issuesDBSteps.ConsultaBugDB(projetoCriado.ProjectId, summary);

            Assert.IsNull(bugRegistradoDB, "O resumo não foi informado, porém um registro de bug foi retornado.");

            string[] arrayRegex = new string[]
            {
                "\"message\":(.*?)\"Summary not specified\"",
                "\"localized\":(.*?)\"A necessary field (.*?)\\\"summary(.*?)\\\"(.*?)",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }

            projectsDBSteps.DeletaProjetoDB(projetoCriado.ProjectId);
        }

        [Test]
        public void CriandoUmBugDescricaoEmBranco()
        {
            #region Inserindo um novo projeto
            string projectName = "CriandoUmBugDescricaoEmBranco";
            var projetoCriado = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Parameters
            string summary = "CriandoUmProblemaDescricaoEmBranco";
            string description = "";
            string additionalInformation = "More info about the issue";
            string tagName = "mantishub";

            //Resultado Esperadp
            string statusCodeExpected = "BadRequest";
            #endregion

            var response = CreateAnIssueSteps.CriandoUmBug(summary, description, additionalInformation, projetoCriado.ProjectId, projectName, tagName, statusCodeExpected);

            var bugRegistradoDB = issuesDBSteps.ConsultaBugDB(projetoCriado.ProjectId, summary);

            Assert.IsNull(bugRegistradoDB, "Uma descrição não foi informada, porém um registro de bug foi retornado.");

            string[] arrayRegex = new string[]
            {
                "\"message\":(.*?)\"Description not specified\"",
                "\"localized\":(.*?)\"A necessary field (.*?)\\\"description(.*?)\\\"(.*?)",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }

            projectsDBSteps.DeletaProjetoDB(projetoCriado.ProjectId);
        }

        [Test]
        public void CriandoUmBugNomeTagEmBranco()
        {
            #region Inserindo um novo projeto
            string projectName = "CriandoUmBugNomeTagEmBranco";
            var projetoCriado = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Parameters
            string summary = "CriandoUmBugNomeTagEmBranco";
            string description = "Description for sample REST issue.";
            string additionalInformation = "More info about the issue";
            string tagName = "";

            //Resultado Esperadp
            string statusCodeExpected = "BadRequest";
            #endregion

            var response = CreateAnIssueSteps.CriandoUmBug(summary, description, additionalInformation, projetoCriado.ProjectId, projectName, tagName, statusCodeExpected);

            var bugRegistradoDB = issuesDBSteps.ConsultaBugDB(projetoCriado.ProjectId, summary);

            Assert.IsNull(bugRegistradoDB, "O nome da tag não foi informado, porém um registro de bug foi retornado.");

            string[] arrayRegex = new string[]
            {
                "\"message\":\"Tag name (.*?)''(.*?) is not valid.\"",
                "localized\":\"Tag name (.*?)\\\"(.*?)\\\"(.*?) is invalid.\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }

            projectsDBSteps.DeletaProjetoDB(projetoCriado.ProjectId);
        }
    }
}