using PFMApi.Data;
using PFMApi.Data.Contracts;
using PFMApi.Data.Entities;
using PFMApi.Dto;
using PFMApi.Helpers;

using PFMApi.Services;
using PFMApi.Services.Contracts;
using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace PFMApi
{
    public class Startup
    {
        // IConfiguration reads the data from the appsettings.json and dev version 
        public IConfiguration Configuration { get; }
        private IServiceCollection Services { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Connection to Database, type of driver config 
            var dbVer = Configuration["UseDbVer"]; // lokacija app.settings.json
            services.AddDbContext<AppDbContext>(x =>
            {
                if (dbVer.Equals("PostgreSql"))
                    x.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                else if (dbVer.Equals("MySql"))
                    x.UseMySql(Configuration.GetConnectionString("DefaultConnectionMySql"));
            });

            // Register services in the app
            services.AddControllers();
            services.AddHttpClient();
            services.AddCors();
            services.AddAutoMapper(typeof(Startup).Assembly);

            // Dependency injection resolvers for services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITransactionsService, TransactionsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();

            Services = services;
        }

        // Activate middlewares to the dotnet core pipe, NOTE!!! Order mathers
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var ServicesProvider = Services.BuildServiceProvider();


            //app.ConfigureCustomExceptionMiddleware(loggerService); // if we wanna use custom midleware 

            app.UseHttpsRedirection();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
