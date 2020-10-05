using RestSharp;
using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Domain;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutomacaoApiMantis.DBSteps.Users;
using AutomacaoApiMantis.Requests.Users;

namespace AutomacaoApiMantis.Tests.Users
{
    [TestFixture]
    public class DeleteUserTests : TestBase
    {
        public UsersDBSteps usersDBSteps = new UsersDBSteps();

        [Test]
        public void DeletaUsuarioComSucesso()
        {
            #region Inserindo um novo usuário
            string username = "DeletaUsuarioComSucesso";
            usersDBSteps.InseriUsuarioDB(username);

            var consultaUsuarioDB = usersDBSteps.ConsultaUsuarioDB(username);
            #endregion

            #region Parameters
            int userId = int.Parse(consultaUsuarioDB.UserId);

            //Resultado Esperado
            string statusCodeExpected = "NoContent";
            #endregion

            DeleteUserRequest deleteUserRequest = new DeleteUserRequest(userId);
            IRestResponse<dynamic> response = deleteUserRequest.ExecuteRequest();

            consultaUsuarioDB = usersDBSteps.ConsultaUsuarioDB(username);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
                Assert.IsNull(consultaUsuarioDB, "O usuário não foi excluído.");
            });
        }

        [Test]
        public void DeletaUsuarioIdQueNaoExiste()
        {
            #region Parameters
            int userId = 00;

            //Resultado Esperado
            string statusCodeExpected = "BadRequest";
            #endregion

            DeleteUserRequest deleteUserRequest = new DeleteUserRequest(userId);
            IRestResponse<dynamic> response = deleteUserRequest.ExecuteRequest();

            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");

            string[] arrayRegex = new string[]
            {
                "\"message\":\"Invalid user id\"",
                "\"localized\":\"Invalid value for 'id'\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }
        }

        [Test]
        public void DeletaUsuarioIdNulo()
        {
            #region Parameters
            int? userId = null;

            //Resultado Esperado
            string statusCodeExpected = "MethodNotAllowed";
            #endregion

            DeleteUserRequest deleteUserRequest = new DeleteUserRequest(userId);
            IRestResponse<dynamic> response = deleteUserRequest.ExecuteRequest();

            Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString(), "O StatusCode retornado não é o esperado.");
        }

    }
}