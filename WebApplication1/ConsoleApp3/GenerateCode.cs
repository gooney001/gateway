using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp3
{
    public class GenerateCode
    {
        public string GetModelString(List<Field> fields)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var f in fields)
            {
                sb.Append("public ");
                switch (f.Type.ToLower())
                {
                    case "char":
                    case "text":
                    case "varchar":
                    case "nchar":
                    case "ntext":
                    case "nvarchar":
                        sb.Append("string ");
                        break;
                    case "smalldatetime":
                    case "datetime":
                        sb.Append("DateTime ");
                        break;
                    case "float":
                        sb.Append("double ");
                        break;
                    case "smallmoney":
                    case "money":
                    case "numeric":
                    case "decimal":
                        sb.Append("decimal ");
                        break;
                    case "tinyint":
                    case "smallint":
                    case "bigint":
                    case "timestamp":
                    case "int":
                        sb.Append("int ");
                        break;
                    case "bit":
                        sb.Append("bool ");
                        break;
                }
                sb.Append($"{f.Name} {{get;set;}}\n");
            }
            return sb.ToString();
        }
    }
}
