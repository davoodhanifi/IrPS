using System;
using IrpsApi.Api.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mabna.WebApi.AspNetCore.BasicAuthentication;
using Mabna.WebApi.AspNetCore.BufferedRequestBodyMiddleware;
using Mabna.WebApi.AspNetCore.Security;
using Mabna.WebApi.AspNetCore.Security.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;

namespace IrpsApi.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStringsOption>(_configuration.GetSection("ConnectionStrings"));
            services.Configure<ApiSessions>(_configuration.GetSection("ApiSessions"));
            services.Configure<SmsSettings>(_configuration.GetSection("SMS"));

            services.RegisterConnectionStrings();
            services.RegisterRepositories();
            services.RegisterServices();

            services.AddSingleton<ConfigurationsPrincipalProvider, ReloadableConfigurationsPrincipalProvider>();
            var sp = services.BuildServiceProvider();
            var principalProvider = sp.GetRequiredService<ConfigurationsPrincipalProvider>();
            services.AddBasicAuthentication(options =>
            {
                options.PrincipalProvider = principalProvider;
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddSingleton(_ => _configuration);

            services.RegisterIPBlacklistHandler();
            services.RegisterIPWhitelistHandler();
            services.AddAuthorization(options =>
            {
                options.AddIPBlacklistPolicy();
                options.AddIPWhitelistPolicy();
            });

            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            services.AddHttpContextAccessor();

            services.AddMvc(options =>
                {
                    options.Filters.Add(new AuthorizeFilter(policy));
                    options.Filters.Add(new AuthorizeFilter(IPBlacklistPolicy.Name));
                    options.Filters.Add(new AuthorizeFilter(IPWhitelistPolicy.Name));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddResponseCompression();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("basic", new BasicAuthScheme());

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "basic", new string[0] }
                });

                c.OperationFilter<SwaggerApiCategorizationFilter>();
                c.OperationFilter<SwaggerConsumesOperationFilter>();

                c.SwaggerDoc("v1", new Info
                {
                    Title = "IrPS API.",
                    Version = "v1",
                    Description = "An API For Iranian Payment Service",
                    Contact = new Contact
                    {
                        Name = "Davood Hanifi",
                        Email = "davood.hanifi@gmail.com",
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IrPS API v1");
            });

            app.UseBufferedRequestBody();
            app.UseAuthentication();
            app.UseMvc();
            app.UseResponseCompression();
        }
    }
}
