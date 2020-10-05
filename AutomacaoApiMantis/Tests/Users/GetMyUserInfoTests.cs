using System;
using RestSharp;
using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.DBSteps.Users;
using AutomacaoApiMantis.Requests.Users;

namespace AutomacaoApiMantis.Tests.Users
{
    [TestFixture]
    public class GetMyUserInfoTests : TestBase
    {
        public UsersDBSteps usersDBSteps = new UsersDBSteps();

        [Test]
        public void ObtendoInformacoesUsuarioLogado()
        {
            #region Parameters
            string statusCodeExpected = "OK";
            string usernameExpected = "administrator";
            #endregion

            GetMyUserInfoRequest getMyUserInfoRequest = new GetMyUserInfoRequest();
            IRestResponse<dynamic> response = getMyUserInfoRequest.ExecuteRequest();

            var consultaUsuarioDB = usersDBSteps.ConsultaUsuarioDB(usernameExpected);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.AreEqual(usernameExpected, consultaUsuarioDB.Username, Convert.ToString(response.Data["name"]), "O usuário não está correto.");
                Assert.AreEqual(consultaUsuarioDB.Email, Convert.ToString(response.Data["email"]), "O e-mail não está correto.");
            });
        }

    }
}