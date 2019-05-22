using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
    public class Startup
    {
        private object builder;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                // Configure supported formatters here 
                // Inputs are controlled by the Accept header 
                // Output are controlled by the ContentType header

                // This allows Accept Header: application/xml
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

                //// ASP. NET Core by default deserializes to JSON with camel case naming.
                //// The added code below will overwrite the default naming strategy of the ContractResolver
                //.AddJsonOptions(o =>
                //{
                //    if (o.SerializerSettings.ContractResolver != null)
                //    {
                //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                //        castedResolver.NamingStrategy = null;  //  this keeps the property names as they are defined on the class (Pascal case)
                //    }
                //})
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //  Since the CreateDefaultBuilder call in the Program class 
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
