using System.IO;
using System.Text;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.DBSteps.Users
{
    public class UsersDBSteps
    {
        public UserDomain ConsultaUsuarioDB(string username)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Users/consultaUsuario.sql", Encoding.UTF8);
            query = query.Replace("$username", username);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Username = " + username);

            return DBHelpers.ObtemRegistroUnico<UserDomain>(query);
        }

        public void DeletaUsuarioDB(string userId)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Users/deletaUsuario.sql", Encoding.UTF8);
            query = query.Replace("$userId", userId);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do usuário = " + userId);

            DBHelpers.ExecuteQuery(query);
        }

        public void InseriUsuarioDB(string username)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Users/inseriUsuario.sql", Encoding.UTF8);
            query = query.Replace("$username", username);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Username = " + username);

            DBHelpers.ExecuteQuery(query);
        }
    }
}