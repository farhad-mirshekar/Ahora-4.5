using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ahora.WebApp.Tools
{
    public static class ModuleGenerator
    {
        public static string EnumService(string moduleName, List<string> dllPaths)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("(function () {");
            sb.Append("\t").AppendLine("angular");
            sb.Append("\t\t").AppendLine($".module('{moduleName}')");
            sb.Append("\t\t").AppendLine(".factory('enumService', enumService);");
            sb.AppendLine("");

            sb.Append("\t").AppendLine("function enumService() { var enumService={};");

            foreach (string dllPath in dllPaths)
            {
                sb.AppendLine().Append("\t\t\t").Append($"//enums in dll: {Path.GetFileName(dllPath)}").AppendLine();

                var assembly = Assembly.LoadFrom(dllPath);
                var enums = assembly.GetTypes().Where(t => t.IsEnum);
                foreach (Type t in enums)
                {
                    sb.Append("\t\t\t").Append("enumService." + t.Name).Append(" = {");
                    var count = 0;
                    var enumValues = System.Enum.GetValues(t);
                    foreach (var val in enumValues)
                    {
                        count++;
                        Type type = System.Enum.GetUnderlyingType(t);
                        var id = Convert.ChangeType(val, type);
                        string name = System.Enum.GetName(t, id);
                        if (id.ToString() != "0")
                            sb.Append(string.Format("\n{3}'{0}': '{1}'{2}", id, name.Replace("___", "،").Replace("__", "‌").Replace("_", " "), count == enumValues.Length ? "" : ",", count == 1 ? "" : "\t\t\t\t"));
                    }

                    sb.Append(string.Format("\n\t\t\t}}{0}", t == enums.Last() && dllPath == dllPaths.Last() ? "" : ",")).AppendLine();
                }
            }

            sb.AppendLine("");
            sb.Append("\t\t").AppendLine("return enumService;");
            sb.Append("\t").AppendLine("}");
            sb.AppendLine("})();");

            return sb.ToString();
        }
    }
}