using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InterfaceProject.Web.Data;
using InterfaceProject.Web.Services;
using InterfaceProject.Model.Monitor;
using InterfaceProject.Model.CoreDB;
using InterfaceProject.DTO;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using InterfaceProject.Web.Extensions;
using InterfaceProject.Web.Assist;

namespace InterfaceProject.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbOptions = new DbContextOptionsBuilder<InterfaceCoreDB>()
          .UseSqlServer(Configuration.GetConnectionString("InterfaceCoreDB")).Options;
            services.AddScoped(s => new InterfaceCoreDB(dbOptions));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<InterfaceMonitorDB>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("InterfaceMonitorDB")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();//设置全局异常处理
            })
            .AddRazorPagesOptions(options =>
            {
                //options.RootDirectory = "/Home";
                options.Conventions.AuthorizeFolder("/Account/Manage");
                options.Conventions.AuthorizePage("/Account/Logout");
            })
            .AddJsonOptions(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
            WxSDK.ServiceInject.ConfigureServices(services);//配置WxSDK的依赖服务
            OtherSDK.ServiceInject.ConfigureServices(services);
            BLL.ServiceInject.ConfigureServices(services);
            //AutoMapper的帮助类初始化
            AutoMapperHelper.Initialize();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.UseRedisLogger();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "apiRoute",
                    template: "api/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "wechatRoute",
                    template: "{controller=we}_{action=chat}/{id?}");
            });
        }
    }
}
