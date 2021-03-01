using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

namespace Sample1
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
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(assembly);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample1", Version = "v1" });
            });
            services.AddWorkflow(x =>
            {
                var connectionString = Configuration.GetConnectionString("Default");
                x.UsePostgreSQL(connectionString, true, true);
            });
            services.AddWorkflowDSL();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            LoadWorkflow(app);
        }

        private void LoadWorkflow(IApplicationBuilder app)
        {
            const string workFlowFileName = "Sample1.BusinessLogic.Workflows.Order.orderWorkflow.json";
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(workFlowFileName);

            if (stream == null)
            {
                throw new FileNotFoundException("Cannot find workflow file.", workFlowFileName);
            }

            using var reader = new StreamReader(stream);
            var workflowJson = reader.ReadToEnd();

            var loader = app.ApplicationServices.GetService<IDefinitionLoader>();

            if (loader is null)
            {
                throw new ArgumentNullException(nameof(loader), "IDefinitionLoader is null in application");
            }

            loader.LoadDefinition(workflowJson, Deserializers.Json);
        }
    }
}
