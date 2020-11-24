using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Helpers;
using AutomacaoApiMantis.Steps.Users;
using AutomacaoApiMantis.DBSteps.Users;

namespace AutomacaoApiMantis.Tests.Users
{
    [TestFixture]
    public class CreateUserTests : TestBase
    {
        public UsersDBSteps usersDBSteps = new UsersDBSteps();

        [Test]
        [TestCaseSource(typeof(DataDrivenHelpers), "ReturnUserValid_XLSX")]
        public void CriandoUsuarioValido(UserDomain coluna)
        {
            #region Parameters
            string username = coluna.Username;
            string password = coluna.Password;
            string realName = coluna.RealName;
            string email = coluna.Email;
            string acessLevelName = coluna.AccessLevel;
            string enabled = coluna.Enabled;
            string protecao = coluna.Protected;

            //Resultado esperado
            string statusCodeExpected = "Created";
            #endregion

            var response = CreateUserSteps.CriandoUsuarioComSucesso(username, password, realName, email, acessLevelName, enabled, protecao, statusCodeExpected);
            var consultaUsuarioDB = usersDBSteps.ConsultaUsuarioDB(username);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(consultaUsuarioDB.Username, response.Username, username, "O usuário não está correto.");
                Assert.AreEqual(consultaUsuarioDB.RealName, response.RealName, realName, "O nome do usuário não está correto.");
                Assert.AreEqual(consultaUsuarioDB.Email, response.Email, email, "O e-mail não está correto.");
            });

            usersDBSteps.DeletaUsuarioDB(consultaUsuarioDB.UserId);
        }

        [Test]
        [TestCaseSource(typeof(DataDrivenHelpers), "ReturnUserWithEmailInvalid_XLSX")]
        public void CriandoUsuarioEmailInvalido(UserDomain coluna)
        {
            #region Parameters
            string username = coluna.Username;
            string password = coluna.Password;
            string realName = coluna.RealName;
            string email = coluna.Email;
            string acessLevelName = coluna.AccessLevel;
            string enabled = coluna.Enabled;
            string protecao = coluna.Protected;

            //Resultado esperado
            string statusCodeExpected = "BadRequest";
            string messageExpected = "Email '" + email + "' is disposable.";
            string localizedExpected = "It is not allowed to use disposable e-mail addresses.";
            #endregion

            var response = CreateUserSteps.CriandoUsuarioSemSucesso(username, password, realName, email, acessLevelName, enabled, protecao, statusCodeExpected);
            var consultaUsuarioDB = usersDBSteps.ConsultaUsuarioDB(username);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(messageExpected, response.DataMessage, "A message não é a esperada.");
                Assert.AreEqual(localizedExpected, response.DataLocalized, "O localized não é o esperado.");
                Assert.IsNull(consultaUsuarioDB, "Usuário existente no banco.");
            });
        }

        [Test]
        [TestCaseSource(typeof(DataDrivenHelpers), "ReturnUserWithUsernameInvalid_XLSX")]
        public void CriandoUsuarioSemInformarNomeUsuario(UserDomain coluna)
        {
            #region Parameters
            string username = coluna.Username;
            string password = coluna.Password;
            string realName = coluna.RealName;
            string email = coluna.Email;
            string acessLevelName = coluna.AccessLevel;
            string enabled = coluna.Enabled;
            string protecao = coluna.Protected;

            //Resultado esperado
            string statusCodeExpected = "BadRequest";
            string messageExpected = "Invalid username ''";
            string localizedExpected = "The username is invalid. Usernames may only contain Latin letters, numbers, spaces, hyphens, dots, plus signs and underscores.";
            #endregion

            var response = CreateUserSteps.CriandoUsuarioSemSucesso(username, password, realName, email, acessLevelName, enabled, protecao, statusCodeExpected);
            var consultaUsuarioDB = usersDBSteps.ConsultaUsuarioDB(username);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(messageExpected, response.DataMessage, "A message não é a esperada.");
                Assert.AreEqual(localizedExpected, response.DataLocalized, "O localized não é o esperado.");
                Assert.IsNull(consultaUsuarioDB, "Usuário existente no banco.");
            });
        }

        [Test]
        [TestCaseSource(typeof(DataDrivenHelpers), "ReturnUserWithAccessLevelInvalid_XLSX")]
        public void CriandoUsuarioSemInformarNivelAcesso(UserDomain coluna)
        {
            #region Parameters
            string username = coluna.Username;
            string password = coluna.Password;
            string realName = coluna.RealName;
            string email = coluna.Email;
            string acessLevelName = coluna.AccessLevel;
            string enabled = coluna.Enabled;
            string protecao = coluna.Protected;

            //Resultado esperado
            string statusCodeExpected = "BadRequest";
            string messageExpected = "Invalid access level";
            string localizedExpected = "Invalid value for 'access_level'";
            #endregion

            var response = CreateUserSteps.CriandoUsuarioSemSucesso(username, password, realName, email, acessLevelName, enabled, protecao, statusCodeExpected);
            var consultaUsuarioDB = usersDBSteps.ConsultaUsuarioDB(username);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(messageExpected, response.DataMessage, "A message não é a esperada.");
                Assert.AreEqual(localizedExpected, response.DataLocalized, "O localized não é o esperado.");
                Assert.IsNull(consultaUsuarioDB, "Usuário existente no banco.");
            });
        }
    }
}