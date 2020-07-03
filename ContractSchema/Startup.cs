using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContractSchema.Repository;
using ContractSchema.Types;
using Demo.Contracts;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContractSchema
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ContractRepository>();

            // Add GraphQL Services
            services.AddGraphQL(Schema.Create(c =>
            {
                c.RegisterQueryType<QueryType>();
                c.RegisterType<LifeInsuranceContractType>();
                c.RegisterType<SomeOtherContractType>();

                // c.UseGlobalObjectIdentifier();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL();
            
            app.UsePlayground();

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
