using RestSharp;
using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.DBSteps.Filters;
using AutomacaoApiMantis.Requests.Filters;

namespace AutomacaoApiMantis.Tests.Filters
{
    [TestFixture]
    public class DeleteAFilterTests : TestBase
    {
        public FiltersDBSteps filtersDBSteps = new FiltersDBSteps();

        [Test]
        public void DeletandoFiltroExistente()
        {
            #region Parameters
            string filterName = "DeletandoFiltroExistenteTest";
            var inseriFiltroPublicoDB = filtersDBSteps.InseriFiltroPublicoDB(filterName);
       
            //Resultado Esperado
            string statusCodeExpected = "NoContent";
            #endregion

            DeleteAFilterRequest deleteAFilterRequest = new DeleteAFilterRequest(inseriFiltroPublicoDB.FilterId);
            IRestResponse<dynamic> response = deleteAFilterRequest.ExecuteRequest();

            var consultaFiltroDB = filtersDBSteps.ConsultaFiltroDB(inseriFiltroPublicoDB.FilterId);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.IsNull(consultaFiltroDB, "O filtro não foi apagado.");      
            });
        }

        [Test]
        public void DeletandoFiltroQueNaoExiste()
        {
            #region Parameters
            int? FilterId = 0;

            //Resultado Esperado
            string statusCodeExpected = "NotFound";
            string statusDescriptionExpected = "Filter not found";
            #endregion

            DeleteAFilterRequest deleteAFilterRequest = new DeleteAFilterRequest(FilterId);
            IRestResponse<dynamic> response = deleteAFilterRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.AreEqual(statusDescriptionExpected, response.StatusDescription, "O StatusDescription retornado não é o esperado.");
            });
        }
    }
}