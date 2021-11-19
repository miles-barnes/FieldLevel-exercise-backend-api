using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fieldlevel.Services;
using Fieldlevel.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fieldlevel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fieldlevel", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fieldlevel v1"));
            }

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterType<PostService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<RemotePostProvider>().As<IPostProvider>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CachingPostProviderDecorator, IPostProvider>();
            builder.RegisterType<MemoryCache>().As<IMemoryCache>().SingleInstance();

            builder.Register(context => new RemoteServiceSettings { PostsUrl = Configuration["PostService:ServiceUrl"] })
                .AsSelf().InstancePerLifetimeScope();
            builder.Register(context => new CacheSettings { PostsCacheMinutes = int.Parse(Configuration["PostService:CacheMinutes"]) })
                .AsSelf().InstancePerLifetimeScope();
        }
    }
}
