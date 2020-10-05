using System;
using System.IO;
using NUnit.Framework;
using System.Data.OleDb;
using System.Collections.Generic;

namespace AutomacaoApiMantis.Helpers
{
    public class ExcelHelpers
    {
        public List<TestCaseData> ReadExcelData(string excelFile, string cmdText = "SELECT * FROM [Tab1$]")
        {
            string connectionStr = ConnectionStringExcel(excelFile);
            var ret = new List<TestCaseData>();

            using (var connection = new OleDbConnection(connectionStr))
            {
                connection.Open();
                var command = new OleDbCommand(cmdText, connection);
                var reader = command.ExecuteReader();
                if (reader == null)
                    throw new Exception(string.Format("No data return from file, file name:{0}", excelFile));

                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                    {
                        var row = new List<string>();
                        var feildCnt = reader.FieldCount;

                        for (var i = 0; i < feildCnt; i++)

                            row.Add(reader.GetValue(i).ToString());

                        ret.Add(new TestCaseData(row.ToArray()));

                    }
                }
                return ret;
            }
        }

        public static String ConnectionStringExcel(string excelFile)
        {

            if (!File.Exists(excelFile))
                throw new Exception(string.Format("File name: {0}", excelFile), new FileNotFoundException());
            string connectionStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", excelFile);

            return connectionStr;
        }
    }
}