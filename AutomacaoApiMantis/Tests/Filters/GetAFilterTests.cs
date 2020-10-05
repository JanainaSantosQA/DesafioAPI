using System;
using RestSharp;
using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.DBSteps.Filters;
using AutomacaoApiMantis.Requests.Filters;

namespace AutomacaoApiMantis.Tests.Filters
{
    [TestFixture]
    public class GetAFilterTests : TestBase
    {
        public FiltersDBSteps filtersDBSteps = new FiltersDBSteps();

        [Test]
        public void ConsultandoUmFiltroEspecifico()
        {
            #region Parameters
            string filterName = "ConsultandoUmFiltroEspecificoTest";
            var inseriFiltroPublicoDB = filtersDBSteps.InseriFiltroPublicoDB(filterName);

            //Resultado Esperado
            string statusCodeExpected = "OK";
            #endregion

            GetAFilterRequest getAFilterRequest = new GetAFilterRequest(inseriFiltroPublicoDB.FilterId);
            IRestResponse<dynamic> response = getAFilterRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.AreEqual(inseriFiltroPublicoDB.FilterId.ToString(), Convert.ToString(response.Data["filters"][0]["id"]), "O id não é o esperado.");
                Assert.AreEqual(filterName, inseriFiltroPublicoDB.FilterName, Convert.ToString(response.Data["filters"][0]["name"]), "O nome do filtro não é o esperado.");
            });

            filtersDBSteps.DeletaFiltroDB(inseriFiltroPublicoDB.FilterId);
        }
        [Test]
        public void ConsultandoUmFiltroQueNaoExiste()
        {
            #region Parameters
            int? filterId = 0;

            //Resultado Esperado
            string statusCodeExpected = "OK";
            string responseContentExpected = "{\"filters\":[]}";
            #endregion

            GetAFilterRequest getAFilterRequest = new GetAFilterRequest(filterId);
            IRestResponse<dynamic> response = getAFilterRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.AreEqual(responseContentExpected, response.Content, "Um filtro foi retornado.");
            });
        }
        [Test]
        public void ConsultandoUmFiltroEspecificoNaoPublico()
        {
            #region Parameters
            string filterName = "ConsultandoUmFiltroEspecificoQueNaoEstaPublicoTest";
            var inseriFiltroPrivadoDB = filtersDBSteps.InseriFiltroPrivadoDB(filterName);

            //Resultado Esperado
            string statusCodeExpected = "OK";
            string responseContentExpected = "{\"filters\":[]}";
            #endregion

            GetAFilterRequest getAFilterRequest = new GetAFilterRequest(inseriFiltroPrivadoDB.FilterId);
            IRestResponse<dynamic> response = getAFilterRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.AreEqual(responseContentExpected, response.Content, "Um filtro foi retornado.");
            });

            filtersDBSteps.DeletaFiltroDB(inseriFiltroPrivadoDB.FilterId);
        }
    }
}