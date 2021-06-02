using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

using esdc_rules_api.Lib;
using esdc_rules_api.MaternityBenefits;
using esdc_rules_api.MaternityBenefits.Classes;
using esdc_rules_api.SampleScenario;
using esdc_rules_api.SampleScenario.Classes;
using esdc_rules_api.OpenFisca;
using RestSharp;

namespace esdc_rules_api
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
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EI Rules API",
                    Description = "https://laws-lois.justice.gc.ca/eng/acts/e-5.6/",
                    TermsOfService = new Uri("https://example.com/terms")
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            InjectMaternityBenefits(services);
            InjectSampleScenario(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/spec.json", "EI Rules Spec");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InjectMaternityBenefits(IServiceCollection services) {
            
            services.AddScoped<IHandleRequests<MaternityBenefitsCase, MaternityBenefitsPerson>, RequestHandler<MaternityBenefitsCase, MaternityBenefitsPerson>>();
            //services.AddScoped<ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson>, MaternityBenefitsDefaultCalculator>();
            services.AddScoped<ICalculateRules<MaternityBenefitsCase, MaternityBenefitsPerson>, MaternityBenefitsOpenFiscaCalculator>();

            // OpenFisca
            services.AddScoped<IOpenFisca, OpenFiscaLib>();
            services.AddScoped<IRestClient, RestSharp.RestClient>();

            // OpenFisca options
            var openFiscaUrl = Configuration.GetValue<string>("OpenFiscaOptions:Url") ?? 
                Environment.GetEnvironmentVariable("openFiscaUrl");
                
            var openFiscaOptions = new OpenFiscaOptions() {
                Url = openFiscaUrl
            };
            services.AddSingleton<IOptions<OpenFiscaOptions>>(x => Options.Create(openFiscaOptions));

        }

        private void InjectSampleScenario(IServiceCollection services) {
            
            services.AddScoped<IHandleRequests<SampleScenarioCase, SampleScenarioPerson>, RequestHandler<SampleScenarioCase, SampleScenarioPerson>>();
            services.AddScoped<ICalculateRules<SampleScenarioCase, SampleScenarioPerson>, SampleScenarioDefaultCalculator>();
        }
    }
}
