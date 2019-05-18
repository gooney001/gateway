using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostcontext,build)=> {
                var config = build.Build();
                var env = hostcontext.HostingEnvironment;
                build.SetBasePath(hostcontext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("configuration.json")
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true,reloadOnChange:true)
                .AddEnvironmentVariables();
            }).UseStartup<Startup>().UseUrls("http://*:50002");
    }
}
