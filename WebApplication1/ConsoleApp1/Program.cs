using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            List<Task> tasks = new List<Task>();
            stopwatch.Start();
            for (int i = 0; i < 100; i++)
            {
                string url = "http://localhost:50002/api/file/test/?name="+i.ToString();
                tasks.Add(Task.Run(() => {
                    //string r=HttpRequest.DoGet1(url);
                    var rr = HttpRequest.DoGet(url);
                    var r=rr.Result.Content.ReadAsStringAsync().Result;
                    //HttpRequest.HttpGet(url);
                    Console.WriteLine(url+"#"+r);
                }));
            }
            Task.WaitAll(tasks.ToArray());
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
