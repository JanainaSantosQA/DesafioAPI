using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AutomacaoApiMantis.Helpers
{
    public class GeneralHelpers
    {
        public static string FormatJson(string str)
        {
            string INDENT_STRING = "    ";
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        public static void EnsureDirectoryExists(string fullReportFilePath)
        {
            var directory = Path.GetDirectoryName(fullReportFilePath);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }

        public static string ReturnProjectPath()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;

            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));

            return new Uri(actualPath).LocalPath;
        }

        public static bool VerificaSeStringEstaContidaNaLista(List<string> lista, string p_string)
        {
            foreach (string item in lista)
            {
                if (item.Equals(p_string))
                {
                    return true;
                }
            }
            return false;
        }

        public static int RetornaNumeroDeObjetosDoJson(JArray json)
        {
            return json.Count;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetMethodNameByLevel(int level)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(level);
            return sf.GetMethod().Name;
        }

        public static bool IsAJsonArray(string json)
        {
            if (json.StartsWith("["))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ValidaStatusCodeComComandoNodeJS(string statusCodeExpected, string statusCodeReturned)
        {
            string caminhoArquivo = GeneralHelpers.ReturnProjectPath() + "Resources\\ValidaStatusCodeJS.js";

            var linhas = File.ReadAllLines(caminhoArquivo);

            linhas[2] = "var statusCodeExpected = \"" + statusCodeExpected + "\";";
            linhas[3] = "var statusCodeReturned = \"" + statusCodeReturned + "\";";

            File.WriteAllLines(caminhoArquivo, linhas);

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files\nodejs\node.exe";
            start.Arguments = caminhoArquivo;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();

                    if (!result.Contains("true"))
                    {
                        throw new Exception("Status Code esperado: " + statusCodeExpected + " Status Code retornado: " + statusCodeReturned);
                    }
                }

            }
        }
    }

    static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }

    public class Global
    {
        public static string dbUrl;
        public static string dbPort;
        public static string dbName;
        public static string dbUser;
        public static string dbPassword;

        public static string url;
        public static string token;

        public void Initializer()
        {
            if (JsonBuilder.ReturnParameterAppSettings("ENVIROMENT") == "QA" || JsonBuilder.ReturnParameterAppSettings("ENVIROMENT") == "qa")
            {

                dbUrl = JsonBuilder.ReturnParameterAppSettings("DB_URL_QA");
                dbPort = JsonBuilder.ReturnParameterAppSettings("DB_PORT_QA");
                dbName = JsonBuilder.ReturnParameterAppSettings("DB_NAME_QA");
                dbUser = JsonBuilder.ReturnParameterAppSettings("DB_USER_QA");
                dbPassword = JsonBuilder.ReturnParameterAppSettings("DB_PASSWORD_QA");

                url = JsonBuilder.ReturnParameterAppSettings("URL_QA");
                token = JsonBuilder.ReturnParameterAppSettings("TOKEN_QA");
            }
            else
            {
                dbUrl = JsonBuilder.ReturnParameterAppSettings("DB_URL_HML");
                dbPort = JsonBuilder.ReturnParameterAppSettings("DB_PORT_HML");
                dbName = JsonBuilder.ReturnParameterAppSettings("DB_NAME_HML");
                dbUser = JsonBuilder.ReturnParameterAppSettings("DB_USER_HML");
                dbPassword = JsonBuilder.ReturnParameterAppSettings("DB_PASSWORD_HML");

                url = JsonBuilder.ReturnParameterAppSettings("URL_HML");
                token = JsonBuilder.ReturnParameterAppSettings("TOKEN_HML");
            }
        }
    }
}