using System.Linq;
using NUnit.Framework;
using AutomacaoApiMantis.Domain;
using System.Collections.Generic;

namespace AutomacaoApiMantis.Helpers
{
    public class DataDrivenHelpers
    {
        public static IEnumerable<UserDomain> ReturnUserValid_XLSX
        {
            get
            {
                var testCases = new List<TestCaseData>();
                var userDomains = new List<UserDomain>();
                testCases = new ExcelHelpers().ReadExcelData(GeneralHelpers.ReturnProjectPath() + @"DataDriven\Users\UserValid.xlsx");

                userDomains = testCases.Select(t =>
                                   new UserDomain
                                   {
                                       Username = t.Arguments[0].ToString(),
                                       Password = t.Arguments[1].ToString(),
                                       RealName = t.Arguments[2].ToString(),
                                       Email = t.Arguments[3].ToString(),
                                       AccessLevel = t.Arguments[4].ToString(),
                                       Enabled = t.Arguments[5].ToString(),
                                       Protected = t.Arguments[6].ToString(),
                                   }).ToList();

                if (testCases != null)
                {
                    foreach (UserDomain testCaseData in userDomains)
                        yield return testCaseData;
                }
            }
        }

        public static IEnumerable<UserDomain> ReturnUserWithEmailInvalid_XLSX
        {
            get
            {
                var testCases = new List<TestCaseData>();
                var userDomains = new List<UserDomain>();
                testCases = new ExcelHelpers().ReadExcelData(GeneralHelpers.ReturnProjectPath() + @"DataDriven\Users\UserWithEmailInvalid.xlsx");

                userDomains = testCases.Select(t =>
                                   new UserDomain
                                   {
                                       Username = t.Arguments[0].ToString(),
                                       Password = t.Arguments[1].ToString(),
                                       RealName = t.Arguments[2].ToString(),
                                       Email = t.Arguments[3].ToString(),
                                       AccessLevel = t.Arguments[4].ToString(),
                                       Enabled = t.Arguments[5].ToString(),
                                       Protected = t.Arguments[6].ToString(),
                                   }).ToList();

                if (testCases != null)
                {
                    foreach (UserDomain testCaseData in userDomains)
                        yield return testCaseData;
                }
            }
        }

        public static IEnumerable<UserDomain> ReturnUserWithUsernameInvalid_XLSX
        {
            get
            {
                var testCases = new List<TestCaseData>();
                var userDomains = new List<UserDomain>();
                testCases = new ExcelHelpers().ReadExcelData(GeneralHelpers.ReturnProjectPath() + @"DataDriven\Users\UserWithUsernameInvalid.xlsx");

                userDomains = testCases.Select(t =>
                                   new UserDomain
                                   {
                                       Password = t.Arguments[0].ToString(),
                                       RealName = t.Arguments[1].ToString(),
                                       Email = t.Arguments[2].ToString(),
                                       AccessLevel = t.Arguments[3].ToString(),
                                       Enabled = t.Arguments[4].ToString(),
                                       Protected = t.Arguments[5].ToString(),
                                   }).ToList();

                if (testCases != null)
                {
                    foreach (UserDomain testCaseData in userDomains)
                        yield return testCaseData;
                }
            }
        }

        public static IEnumerable<UserDomain> ReturnUserWithAccessLevelInvalid_XLSX
        {
            get
            {
                var testCases = new List<TestCaseData>();
                var userDomains = new List<UserDomain>();
                testCases = new ExcelHelpers().ReadExcelData(GeneralHelpers.ReturnProjectPath() + @"DataDriven\Users\UserWithAccessLevelInvalid.xlsx");

                userDomains = testCases.Select(t =>
                                   new UserDomain
                                   {
                                       Username = t.Arguments[0].ToString(),
                                       Password = t.Arguments[1].ToString(),
                                       RealName = t.Arguments[2].ToString(),
                                       Email = t.Arguments[3].ToString(),
                                       AccessLevel = t.Arguments[4].ToString(),
                                       Enabled = t.Arguments[5].ToString(),
                                       Protected = t.Arguments[6].ToString(),
                                   }).ToList();

                if (testCases != null)
                    foreach (UserDomain testCaseData in userDomains)
                        yield return testCaseData;
            }
        }
    }
}