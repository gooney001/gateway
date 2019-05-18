using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("输入表名：");
            string tableName = Console.ReadLine();
            GetFiled getFiled = new GetFiled();
            var r = getFiled.GetFileds("server=192.168.240.250;database=YeeSkyGo;UID=testdb_write;pwd=Dbsfqdw!123456", tableName);
            var generateCode = new GenerateCode();
            var s = generateCode.GetModelString(r);
            Console.WriteLine(s);
            Console.Read();
        }
    }
}
