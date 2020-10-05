using System;
using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Steps.Projects;
using AutomacaoApiMantis.DBSteps.Projects;

namespace AutomacaoApiMantis.Tests.Projects
{
    [TestFixture]
    public class UpdateSubProjectTests : TestBase
    {
        ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();

        [Test]
        public void AtualizandoSubProjetoValido()
        {
            #region Inserindo novo projeto e novo subprojeto
            string subProjectParentName = "AtualizandoSubProjetoValidoProjectParent";
            var subProjectParent = projectsDBSteps.InseriProjetoDB(subProjectParentName);

            string subProjetoChildName = "AtualizandoSubProjetoValidoProjetoChild";
            var subProjectChild = projectsDBSteps.InseriProjetoDB(subProjetoChildName);

            string inheritSubProjectParent = "1";

            projectsDBSteps.InseriSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId, inheritSubProjectParent);
            #endregion

            #region Parameters

            //Resultado esperado
            int inheritParentExpected = 0;
            string statusCodeExpected = "NoContent";
            string statusDescriptionExpected = "Subproject '" + subProjectChild.ProjectId + "' updated";
            #endregion

            var response = UpdateSubProjectSteps.AtualizandoUmSubProjetoComSucesso(subProjectParent.ProjectId, subProjectChild.ProjectId, subProjetoChildName, inheritParentExpected, statusCodeExpected);

            var consultaSubProjetoDB = projectsDBSteps.ConsultaSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O status description não é o esperado.");
                Assert.AreEqual(inheritParentExpected, consultaSubProjetoDB.InheritParent, "O inherit parent não foi atualizado corretamente.");
            });

            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ParentId);
            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ChildId);
            projectsDBSteps.DeletaSubProjetoDB(consultaSubProjetoDB.ChildId, consultaSubProjetoDB.ParentId);
        }

        [Test]
        public void AtualizandoSubProjetoIdProjetoPaiNaoExiste()
        {
            #region Inserindo novo projeto e novo subprojeto
            string subProjectParentName = "AtualizandoSubProjetoIdProjectParentNaoExiste";
            var subProjectParent = projectsDBSteps.InseriProjetoDB(subProjectParentName);

            string subProjetoChildName = "AtualizandoSubProjetoIdProjectParentNaoExisteChild";
            var subProjectChild = projectsDBSteps.InseriProjetoDB(subProjetoChildName);

            string inheritSubProjectParent = "1";

            projectsDBSteps.InseriSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId, inheritSubProjectParent);
            #endregion

            #region Parameters
            int projectParentId = 9999;
            int inheritParent = 0;

            //Resultado esperado
            string statusCodeExpected = "NotFound";
            string messageResponseDataExpected = "Project '" + projectParentId + "' not found";
            #endregion

            var response = UpdateSubProjectSteps.AtualizandoUmSubProjetoSemSucesso(projectParentId, subProjectChild.ProjectId, subProjetoChildName, inheritParent, statusCodeExpected);

            var consultaSubProjetoDB = projectsDBSteps.ConsultaSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(consultaSubProjetoDB.InheritParent, Convert.ToInt32(inheritSubProjectParent), "Apesar de ter informado o id de um projeto pai que não existe o inherit parent foi atualizado.");
                Assert.AreEqual(messageResponseDataExpected, response.DataMessage, "O dado da mensagem não é o esperado.");
            });

            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ParentId);
            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ChildId);
            projectsDBSteps.DeletaSubProjetoDB(consultaSubProjetoDB.ChildId, consultaSubProjetoDB.ParentId);
        }

        [Test]
        public void AtualizandoSubProjetoInvalido()
        {
            #region Inserindo novo projeto e novo subprojeto
            string subProjectParentName = "AtualizandoSubProjetoIdProjectParentNaoExiste";
            var subProjectParent = projectsDBSteps.InseriProjetoDB(subProjectParentName);

            string subProjetoChildName = "AtualizandoSubProjetoIdProjectParentNaoExisteChild";
            var subProjectChild = projectsDBSteps.InseriProjetoDB(subProjetoChildName);

            string inheritSubProjectParent = "1";
            projectsDBSteps.InseriSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId, inheritSubProjectParent);
            #endregion

            #region Parameters
            int inheritParent = 0;

            //Resultado esperado
            string statusCodeExpected = "BadRequest";
            string messageResponseDataExpected = "Project '" + subProjectParent.ProjectId + "' is not a subproject of '" + subProjectChild.ProjectId + "'";
            #endregion

            var response = UpdateSubProjectSteps.AtualizandoUmSubProjetoSemSucesso(subProjectChild.ProjectId, subProjectParent.ProjectId, subProjetoChildName, inheritParent, statusCodeExpected);

            var consultaSubProjetoDB = projectsDBSteps.ConsultaSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId);

            Assert.AreEqual(messageResponseDataExpected, response.DataMessage, "O dado da mensagem não é o esperado.");

            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ParentId);
            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ChildId);
            projectsDBSteps.DeletaSubProjetoDB(consultaSubProjetoDB.ChildId, consultaSubProjetoDB.ParentId);
        }
    }
}