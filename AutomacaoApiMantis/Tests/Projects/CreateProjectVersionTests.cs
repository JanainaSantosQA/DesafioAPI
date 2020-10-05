using System;
using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using System.Collections.Generic;
using AutomacaoApiMantis.Steps.Projects;
using AutomacaoApiMantis.DBSteps.Projects;

namespace AutomacaoApiMantis.Tests.Projects
{
    [TestFixture]
    public class CreateProjectVersionTests : TestBase
    {
        ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();

        [Test]
        public void CriandoVersaoProjeto()
        {
            #region Inserindo um novo projeto
            string projectName = "Criando_Versao_Projeto_Test";
            var projetoCriadoDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Parameters
            string versionName = "v1.0.0";
            string versionDescription = "Major new version";
            int versionReleased = 1;
            int versionObsolete = 0;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd");

            //resultadoEsperado
            string statusCodeExpected = "NoContent";
            #endregion

            string versionIdStatusDescription = CreateProjectVersionSteps.CriandoVersaoProjetoComSucesso(versionName, versionDescription, versionReleased, versionObsolete, timestamp, projetoCriadoDB.ProjectId.ToString(), statusCodeExpected);
            var consultaVersaoProjetoDB = projectsDBSteps.ConsultaVersaoProjetoDB(versionIdStatusDescription);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(consultaVersaoProjetoDB.VersionName, versionName, "O nome da versão registrado no banco não é o esperado.");
                Assert.AreEqual(consultaVersaoProjetoDB.VersionDescription, versionDescription, "A descrição da versão registrada no banco não é o esperada.");
                Assert.AreEqual(consultaVersaoProjetoDB.VersionReleased, versionReleased, "A released da versão registrada no banco não é o esperada.");
                Assert.AreEqual(consultaVersaoProjetoDB.VersionObsolete, versionObsolete, "O obsolete da versão registrado no banco não é o esperado.");
            });

            projectsDBSteps.DeletaVersaoProjetoDB(consultaVersaoProjetoDB.VersionId);
            projectsDBSteps.DeletaProjetoDB(projetoCriadoDB.ProjectId);
        }

        [Test]
        public void CriandoVersaoDuplicadaProjeto()
        {
            #region Inserindo um novo projeto
            string projectName = "Criando_Versao_Duplicada_Projeto";
            var inseriProjectDB = projectsDBSteps.InseriProjetoDB(projectName);
            #endregion

            #region Criando uma nova versão para o projeto
            var inseriVersaoProjetoDB = projectsDBSteps.InseriVersaoProjetoDB(inseriProjectDB.ProjectId);
            #endregion

            #region Parameters
            string versionName = "v1.0.0";
            string versionDescription = "Major new version";
            int versionReleased = 1;
            int versionObsolete = 0;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd");

            //resultadoEsperado
            string statusCodeExpected = "BadRequest";
            string responseDataExpected = "Version '" + versionName + "' already exists";
            #endregion

            var response = CreateProjectVersionSteps.CriandoVersaoProjetoSemSucesso(versionName, versionDescription, versionReleased, versionObsolete, timestamp, inseriProjectDB.ProjectId.ToString(), statusCodeExpected);

            Assert.AreEqual(responseDataExpected, response.DataMessage, "O response data retornado não é o esperado.");

            projectsDBSteps.DeletaVersaoProjetoDB(inseriVersaoProjetoDB.VersionId);
            projectsDBSteps.DeletaProjetoDB(inseriProjectDB.ProjectId);
        }

        [Test]
        public void CriandoVersaoIdProjetoInexistente()
        {
            #region Parameters
            string versionName = "v1.0.0";
            string versionDescription = "Major new version";
            int versionReleased = 1;
            int versionObsolete = 0;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd");
            string projectId = "00";

            //resultadoEsperado
            string statusCodeExpected = "BadRequest";
            string responseDataExpected = "'project_id' must be >= 1";
            #endregion

            var response = CreateProjectVersionSteps.CriandoVersaoProjetoSemSucesso(versionName, versionDescription, versionReleased, versionObsolete, timestamp, projectId, statusCodeExpected);

            Assert.AreEqual(responseDataExpected, response.DataMessage, "O response data retornado não é o esperado.");
        }

        [Test]
        public void CriandoVersaoIdProjetoInvalido()
        {
            #region Parameters
            string versionName = "v1.0.0";
            string versionDescription = "Major new version";
            int versionReleased = 1;
            int versionObsolete = 0;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd");
            string projectId = "teste";

            //resultadoEsperado
            string statusCodeExpected = "BadRequest";
            string responseDataExpected = "'project_id' must be numeric";
            #endregion

            var response = CreateProjectVersionSteps.CriandoVersaoProjetoSemSucesso(versionName, versionDescription, versionReleased, versionObsolete, timestamp, projectId, statusCodeExpected);

            Assert.AreEqual(responseDataExpected, response.DataMessage, "O response data retornado não é o esperado.");
        }

        [Test]
        public void CriandoVersaoNomeVersaoVazio()
        {
            #region Parameters
            string versionName = "";
            string versionDescription = "Major new version";
            int versionReleased = 1;
            int versionObsolete = 0;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd");
            string projectId = "1";

            //resultadoEsperado
            string statusCodeExpected = "BadRequest";
            string responseDataExpected = "Invalid version name";
            #endregion

            var response = CreateProjectVersionSteps.CriandoVersaoProjetoSemSucesso(versionName, versionDescription, versionReleased, versionObsolete, timestamp, projectId, statusCodeExpected);

            Assert.AreEqual(responseDataExpected, response.DataMessage, "O response data retornado não é o esperado.");
        }
    }
}