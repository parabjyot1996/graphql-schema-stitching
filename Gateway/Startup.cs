using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.Execution;   
using HotChocolate.Stitching;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup the clients that shall be used to access the remote endpoints.
            services.AddHttpClient("customer", (sp, client) =>
            {
                // in order to pass on the token or any other headers to the backend schema use the IHttpContextAccessor
                HttpContext context = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                client.BaseAddress = new Uri("http://127.0.0.1:5051");
            });
            services.AddHttpClient("contract", (sp, client) =>
            {
                // in order to pass on the token or any other headers to the backend schema use the IHttpContextAccessor
                HttpContext context = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                client.BaseAddress = new Uri("http://127.0.0.1:5052");
            });

            services.AddHttpContextAccessor();

            services.AddSingleton<IQueryResultSerializer, JsonQueryResultSerializer>();

            services.AddGraphQLSubscriptions();

            services.AddStitchedSchema(builder => builder
                .AddSchemaFromHttp("customer")
                .AddSchemaFromHttp("contract")
                .AddExtensionsFromFile("./Extensions.graphql")
                .AddSchemaConfiguration(c => {
                    c.RegisterType<SomeOtherContractExtension>();
                })
                .RenameType("LifeInsuranceContract", "LifeInsurance")
            );
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

    public class SomeOtherContractExtension: ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("SomeOtherContract");
            descriptor.Field("expiresInDays")
                .Type<NonNullType<StringType>>()
                .Resolver(context => 
                {
                    return "Hello";
                });
        }
    }
}