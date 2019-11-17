using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pretty.Core;
using Pretty.Core.Chains;
using Pretty.Core.Helpers;
using Pretty.Data;
using Pretty.Plugin.CheckCmt;
using Pretty.Web.Inject;
using Pretty.Web.Middleware;
using Pretty.WebFramework;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Hubs;
using System;

namespace Pretty.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string AllowSpecificOrigins = "AllowSpecificOrigins";

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR().AddJsonProtocol();
            services.AddHttpContextAccessor();
            services.AddTransient<AuthFilter>();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            var sqlConnection = "data source=www.shenkang.xyz;database=pretty_blog;uid=root;password=root;";

            services.AddDbContext<EfDbContext>(option => option.UseMySQL(sqlConnection));
            services.AddSession();
            services.AddTransient<EventLogMiddleware>();
            services.AddTransient<IDbContext, EfDbContext>();
            services.AddTransient<IFileHelper, FileHelper>();
            services.AddTransient<IWorkContext, WebWorkContext>();
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ServiceInject());
            builder.RegisterModule(new FactoryInject());
            builder.Populate(services);

            Singletion.Register<CommentHandleChain>();
            Singletion.Get<CommentHandleChain>()
                .AddChain(new SensitiveWordHandler());

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSignalR(options =>
            {
                options.MapHub<ChatHub>("/chatHub");
            });
            app.UseSession();
            app.UseCors(AllowSpecificOrigins);
            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseSpaStaticFiles();

            app.UseMiddleware<EventLogMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }
    }
}
