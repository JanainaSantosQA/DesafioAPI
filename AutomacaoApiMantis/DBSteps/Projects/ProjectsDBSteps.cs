using System.IO;
using System.Text;
using AutomacaoApiMantis.Domain;
using AutomacaoApiMantis.Helpers;

namespace AutomacaoApiMantis.DBSteps.Projects
{
    public class ProjectsDBSteps
    {
        public ProjectDomain ConsultaProjetoDB(int projectId)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projects/consultaProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto = " + projectId);

            return DBHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletaProjetoDB(int projectId)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projects/deletaProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto = " + projectId);

            DBHelpers.ExecuteQuery(query);
        }
        public ProjectDomain InseriProjetoDB(string projectName)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projects/inseriProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectName", projectName);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Nome do projeto = " + projectName);

            return DBHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public ProjectDomain ConsultaVersaoProjetoDB(string versionId)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projects/consultaVersaoProjeto.sql", Encoding.UTF8);
            query = query.Replace("$versionId", versionId);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID da versão criada = " + versionId);

            return DBHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public ProjectDomain InseriVersaoProjetoDB(int projectId)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projects/inseriVersaoProjeto.sql", Encoding.UTF8);
            query = query.Replace("$projectId", projectId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto = " + projectId);

            return DBHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletaVersaoProjetoDB(int versionId)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projects/deletaVersaoProjeto.sql", Encoding.UTF8);
            query = query.Replace("$versionId", versionId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID da versão do projeto = " + versionId);

            DBHelpers.ExecuteQuery(query);
        }
        public ProjectDomain ConsultaSubProjetoDB(int childId, int parentId)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projects/consultaSubProjeto.sql", Encoding.UTF8);
            query = query.Replace("$childId", childId.ToString())
                         .Replace("$parentId", parentId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Child ID = " + childId + " Parent ID = " + parentId);

            return DBHelpers.ObtemRegistroUnico<ProjectDomain>(query);
        }
        public void DeletaSubProjetoDB(int childId, int parentId)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projects/deletaSubProjeto.sql", Encoding.UTF8);
            query = query.Replace("$childId", childId.ToString())
                         .Replace("$parentId", parentId.ToString());

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Child ID = " + childId + " Parent ID = " + parentId);

            DBHelpers.ExecuteQuery(query);
        }
        public void InseriSubProjetoDB(int childId, int parentId, string inheritParent)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/Projects/inseriSubProjeto.sql", Encoding.UTF8);
            query = query.Replace("$childId", childId.ToString())
                         .Replace("$parentId", parentId.ToString())
                         .Replace("$inheritParent", inheritParent);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: ID do projeto pai = " + parentId + " ID do projeto filho = " + childId);

            DBHelpers.ExecuteQuery(query);
        }
    }
}