using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using WebApplication1.Middle;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, "Server=localhost; Database=ReportServer; uid=sa;password=123456;")
                .UseAutoSyncStructure(true)
                .UseLazyLoading(true)
                .UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
                .Build();
        }

        public IConfiguration Configuration { get; }
        public interface ISoftDelete
        {
            bool IsDeleted { get; set; }
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFreeSql>(Fsql);
            var builder = new ContainerBuilder();

            builder.RegisterFreeRepository(
                filter => filter
                    .Apply<ISoftDelete>("softdelete", a => a.IsDeleted == false)
                //开启软删除过滤器，可定义多个全局过滤器
                ,
                this.GetType().Assembly
            //将本项目中所有继承实现的仓储批量注入
            );


            services.AddAuthentication("Bearer")//添加授权模式
                .AddIdentityServerAuthentication(Options => {
                    Options.Authority = "http://localhost:5000";//授权服务器地址
                    Options.RequireHttpsMetadata = false;//是否是https
                    Options.ApiName = "api";
                });
            services.AddOcelot();
            services.AddMvc(o=> {
                o.Filters.Add(typeof(ApiResultFilterAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            builder.Populate(services);
            var container = builder.Build();
            
            return new AutofacServiceProvider(container);
        }
        public IFreeSql Fsql { get; }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.RequestLogger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".log"] = "text/html";
            app.UseStaticFiles(new StaticFileOptions() {
                ContentTypeProvider= provider
            });
            var staticImageFile = new StaticFileOptions();
            staticImageFile.FileProvider = new PhysicalFileProvider(@"C:\Users\me\Documents\WeChat Files\wangjialei910\Image\Image");
            app.UseStaticFiles(staticImageFile);
            var staticFile = new StaticFileOptions();
            staticFile.FileProvider = new PhysicalFileProvider(@"C:\Users\me\Documents\WeChat Files\wangjialei910\Files");
            app.UseStaticFiles(staticFile);
            //app.UseOcelot().Wait();
            app.UseMvc();
            app.Run(async (context)=> {
                await context.Response.WriteAsync("hello");
            });
        }
    }
}
