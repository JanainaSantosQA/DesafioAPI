using NUnit.Framework;
using AutomacaoApiMantis.Bases;
using System.Text.RegularExpressions;
using AutomacaoApiMantis.Steps.Issues;

namespace AutomacaoApiMantis.Tests.Issues
{
    [TestFixture]
    public class AddAnIssueRelatiobshipTests : TestBase
    {
        [Test]
        public void AdicionandoRelacionamentoIdBugInexistente()
        {
            #region Parameters
            string bugId = "999999";
            string typeName = "related-to";

            //Resultado Esperadp
            string statusCodeExpected = "NotFound";
            #endregion

            var response = AddAnIssueRelatiobshipSteps.AdicionandoRelacionamentoBug(bugId, typeName, statusCodeExpected);

            string[] arrayRegex = new string[]

            {
                "\"message\":(.*?)\"Issue (.*?)"+ bugId +"(.*?) not found\"",
                "\"localized\":(.*?)\"Issue "+ bugId +" not found.\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }
        }

        [Test]
        public void AdicionarRelacionamentoNaoInformandoTipo()
        {
            #region Parameters
            string bugId = "999999";
            string typeName = "";

            //Resultado Esperadp
            string statusCodeExpected = "BadRequest";
            #endregion

            var response = AddAnIssueRelatiobshipSteps.AdicionandoRelacionamentoBug(bugId, typeName, statusCodeExpected);

            string[] arrayRegex = new string[]

            {
                "\"message\":(.*?)\"Unknown relationship type ''\"",
                "\"localized\":(.*?)\"Invalid value for 'relationship_type'\"",
            };

            MatchCollection matches;
            foreach (string regex in arrayRegex)
            {
                matches = new Regex(regex).Matches(response.Content);
                Assert.That(matches.Count > 0, "Esperado: " + regex + " Encontrado:" + response.Content);
            }
        }
    }
}