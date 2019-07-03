using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // ASP.NET Core MVC capability is added to our application 
            // by adding a reference to MVC as the services and middleware
            services.AddMvc()
                // Configure supported formatters here 
                // Inputs are controlled by the Accept header 
                // Output are controlled by the ContentType header

                // This allows Accept Header: application/xml
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

            //// ASP.NET Core by default deserializes to JSON with camel case naming.
            //// The added code below will overwrite the default naming strategy of the ContractResolver
            //.AddJsonOptions(o =>
            //{
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;  //  this keeps the property names as they are defined on the class (Pascal case)
            //    }
            //})


            //  Register the mail service with the LocalMailService container
            //  Inject it using the built-in dependency injection system onto the ConfigureServices method
            //  1.) Transient lifetime services are created each time they are requested (for lightweight stateless services)
            //  2.) Scoped lifetime services are created once per request
            //  3.) Singleton lifetime services are created the first time they are requested or if you specify an instance when ConfigureServices is run
            //  Every subsequent request will use the same instance

            // Not using the interface
            services.AddTransient<LocalMailService>();

            // Using the interface
            services.AddTransient<IMailService, MailService>();

            var connectionString = Startup.Configuration["connectionStrings:cityInfoDbConnectionString"];

            //Registering the DbContext so we can use dependency injection
            services.AddDbContext<CityInfoDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<ICityInfoRepository, CityInfoRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Built-in services are available for injection into other pieces of code that live in our application
        // which are simply added as parameters
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //  For logging, the CreateDefaultBuilder call in the Program class 
            //  takes care of adding both the console and debug providers,
            //  there's no need to add them here in the Configure method

            if (env.IsDevelopment())  //--  This ensures that the developer-friendly exception page middleware
                                        //  will only be added when running in devel environment
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();  //--  for developer-friendly status code messages in the browser

            app.UseMvc();

            //app.Run((context) => 
            //{
            //    throw new Exception("Testing 1 2 3");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
