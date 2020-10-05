using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Steps.Projects;
using AutomacaoApiMantis.DBSteps.Projects;

namespace AutomacaoApiMantis.Tests.Projects
{
    [TestFixture]
    public class AddSubProjectTests : TestBase
    {
        public ProjectsDBSteps projectsDBSteps = new ProjectsDBSteps();
        public AddSubProjectSteps addSubProjectSteps = new AddSubProjectSteps();

        [Test]
        public void AdicionandoSubProjetoHerancaPaiHabilitado()
        {
            #region Inserindo novo projeto
            string subProjectParentName = "AddSubProjectParentInheritParentTrue";
            var subProjectParent = projectsDBSteps.InseriProjetoDB(subProjectParentName);

            string subProjetoChildName = "AddSubProjetoChildInheritParentTrue";
            var subProjectChild = projectsDBSteps.InseriProjetoDB(subProjetoChildName);
            #endregion

            #region Parameters
            int inheritParent = 1;

            //Resultado esperado
            string statusCodeExpected = "NoContent";
            string statusDescriptionExpected = "Subproject '" + subProjectChild.ProjectId + "' added to project '" + subProjectParent.ProjectId + "'";
            #endregion

            var response = AddSubProjectSteps.AdicionandoUmSubProjetoComSucesso(subProjectParent.ProjectId, inheritParent, subProjetoChildName, statusCodeExpected);
            var consultaSubProjetoDB = projectsDBSteps.ConsultaSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O StatusDescription retornado não é o esperado.");
                Assert.AreEqual(consultaSubProjetoDB.InheritParent, inheritParent, "O inheritParent não está correto.");
            });

            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ParentId);
            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ChildId);
            projectsDBSteps.DeletaSubProjetoDB(consultaSubProjetoDB.ChildId, consultaSubProjetoDB.ParentId);
        }

        [Test]
        public void AdicionandoSubProjetoHerancaPaiDesabilitado()
        {
            #region Inserindo novo projeto
            string subProjectParentName = "AddSubProjectParentInheritParentFalse";
            var subProjectParent = projectsDBSteps.InseriProjetoDB(subProjectParentName);

            string subProjetoChildName = "AddSubProjetoChildInheritParentFalse";
            var subProjectChild = projectsDBSteps.InseriProjetoDB(subProjetoChildName);
            #endregion

            #region Parameters            

            //Resultado esperado
            int inheritParentExpected = 0;
            string statusCodeExpected = "NoContent";
            string statusDescriptionExpected = "Subproject '" + subProjectChild.ProjectId + "' added to project '" + subProjectParent.ProjectId + "'";
            #endregion

            var response = AddSubProjectSteps.AdicionandoUmSubProjetoComSucesso(subProjectParent.ProjectId, inheritParentExpected, subProjetoChildName, statusCodeExpected);
            var consultaSubProjetoDB = projectsDBSteps.ConsultaSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O StatusDescription retornado não é o esperado.");
                Assert.AreEqual(consultaSubProjetoDB.InheritParent, inheritParentExpected, "O inherit_parent não está correto.");
            });

            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ParentId);
            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ChildId);
            projectsDBSteps.DeletaSubProjetoDB(consultaSubProjetoDB.ChildId, consultaSubProjetoDB.ParentId);
        }

        [Test]
        public void AdicionandoProjetoComoSubProjDeleMesmo()
        {
            #region Inserindo novo projeto
            string subProjectParentName = "AdicionandoUmProjetoComoSubProjetoDeleMesmo";
            var subProjectParent = projectsDBSteps.InseriProjetoDB(subProjectParentName);
            #endregion

            #region Parameters
            int inheritParent = 0;

            //Resultado esperado
            string statusCodeExpected = "BadRequest";
            string statusDescriptionExpected = "Project can't be subproject of itself";
            #endregion

            var response = AddSubProjectSteps.AdicionandoUmSubProjetoSemSucesso(subProjectParent.ProjectId, inheritParent, subProjectParentName, statusCodeExpected);
            var consultaSubProjetoDB = projectsDBSteps.ConsultaSubProjetoDB(subProjectParent.ProjectId, subProjectParent.ProjectId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O StatusDescription retornado não é o esperado.");
                Assert.IsNull(consultaSubProjetoDB, "O retorno foi BadRequest, mas um subprojeto foi adicionado no banco.");
            });

            projectsDBSteps.DeletaProjetoDB(subProjectParent.ProjectId);
        }

        [Test]
        public void AdicionandoSubProjetoFilhoNaoExiste()
        {
            #region Inserindo novo projeto
            string subProjectParentName = "AdicionandoUmSubProjetoFilhoNaoExiste";
            var subProjectParent = projectsDBSteps.InseriProjetoDB(subProjectParentName);
            #endregion

            #region Parameters
            int inheritParent = 1;
            string subProjetoChildName = "ProjetoFilhoNaoExiste";

            //Resultado esperado
            string statusCodeExpected = "NotFound";
            string messageResponseDataExpected = "Project '" + subProjetoChildName + "' not found";
            #endregion

            var response = AddSubProjectSteps.AdicionandoUmSubProjetoSemSucesso(subProjectParent.ProjectId, inheritParent, subProjetoChildName, statusCodeExpected);

            Assert.AreEqual(messageResponseDataExpected, response.DataMessage, "A messagem retornada não é a esperada.");

            projectsDBSteps.DeletaProjetoDB(subProjectParent.ProjectId);
        }

        [Test]
        public void AdicionandoSubProjetoIdProjetoPaiNaoExiste()
        {
            #region Parameters
            int projectParentId = 99999;
            int inheritParent = 1;
            string subProjetoChildName = "AdicionandoUmSubProjetoIdProjetoPaiNaoExiste";

            //Resultado esperado
            string statusCodeExpected = "NotFound";
            string messageResponseDataExpected = "Project '" + projectParentId + "' not found";
            #endregion

            var response = AddSubProjectSteps.AdicionandoUmSubProjetoSemSucesso(projectParentId, inheritParent, subProjetoChildName, statusCodeExpected);

            Assert.AreEqual(messageResponseDataExpected, response.DataMessage, "A messagem retornada não é a esperada.");
        }

        [Test]
        public void AdicionandoSubProjetoIdProjetoPaiNulo()
        {
            #region Parameters
            int projectParentId = 00;
            string subProjetoChildName = "AdicionandoUmSubProjetoIdProjetoPaiNulo";
            int inheritParent = 1;

            //Resultado esperado
            string statusCodeExpected = "BadRequest";
            string messageResponseDataExpected = "'project_id' must be >= 1";
            string localizedResponseDataExpected = "Invalid value for 'project_id'";
            #endregion

            var response = AddSubProjectSteps.AdicionandoUmSubProjetoSemSucesso(projectParentId, inheritParent, subProjetoChildName, statusCodeExpected);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(messageResponseDataExpected, response.DataMessage, "A mensagem retornada não é a esperada.");
                Assert.AreEqual(localizedResponseDataExpected, response.DataLocalized, "O dado localizado retornado não é o esperado.");
            });
        }

        [Test]
        public void AdicionandoNovamenteMesmoSubProjeto()
        {
            #region Inserindo novo projeto e novo subprojeto
            string subProjectParentName = "AdicionandoNovamenteMesmoSubProjetoSubProjectParent";
            var subProjectParent = projectsDBSteps.InseriProjetoDB(subProjectParentName);

            string subProjetoChildName = "AdicionandoNovamenteMesmoSubProjetoSubProjetoChild";
            var subProjectChild = projectsDBSteps.InseriProjetoDB(subProjetoChildName);

            string inheritSubProjectParent = "1";

            projectsDBSteps.InseriSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId, inheritSubProjectParent);
            #endregion

            #region Parameters
            int inheritParent = 1;

            //Resultado esperado
            string statusCodeExpected = "BadRequest";
            string statusDescriptionExpected = "Project '" + subProjectChild.ProjectId + "' is already a subproject of '" + subProjectParent.ProjectId + "'";
            #endregion

            var response = AddSubProjectSteps.AdicionandoUmSubProjetoSemSucesso(subProjectParent.ProjectId, inheritParent, subProjetoChildName, statusCodeExpected);
            var consultaSubProjetoDB = projectsDBSteps.ConsultaSubProjetoDB(subProjectChild.ProjectId, subProjectParent.ProjectId);

            Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O StatusDescription retornado não é o esperado.");

            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ParentId);
            projectsDBSteps.DeletaProjetoDB(consultaSubProjetoDB.ChildId);
            projectsDBSteps.DeletaSubProjetoDB(consultaSubProjetoDB.ChildId, consultaSubProjetoDB.ParentId);
        }
    }
}