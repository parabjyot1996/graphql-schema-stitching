using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSchema.Queries;
using CustomerSchema.Repository;
using CustomerSchema.Resolvers;
using CustomerSchema.Types;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomerSchema
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CustomerRepository>();

            services.AddSingleton<CustomerResolver>();

            // Add GraphQL Services
            services.AddGraphQL(Schema.Create(c =>
            {
                c.RegisterQueryType<Query>();
                c.RegisterType<CustomerType>();
                c.RegisterType<ConsultantType>();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL()
                .UsePlayground();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}