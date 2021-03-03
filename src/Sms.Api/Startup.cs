using AutoMapper;
using System;
using Sms.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sms.Api.Services.MapperConfigs;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore;
using Sms.Api.Services;

namespace Sms.Api
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
            services.AddDbContext<SmsDbContext>(options =>
            {
                var connection = this.Configuration.GetConnectionString(nameof(SmsDbContext));
                options.UseSqlServer(connection);
            });

            services.AddControllers();

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200"));
            });

            AddSwagger(services);

            services.AddAutoMapper(config => DefaultMapper.Config(config), AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<StudentsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(opts => opts.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
            app.UseMvc();
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            UseSwagger(app, provider);
        }

        public void AddSwagger(IServiceCollection services)
        {
            services.AddApiVersioning(opts =>
            {
                opts.AssumeDefaultVersionWhenUnspecified = true;
                opts.DefaultApiVersion = new ApiVersion(1, 0);
                opts.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = false;
            });

            services.AddSwaggerGen();
        }

        public void UseSwagger(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(options => options.SerializeAsV2 = true);

            SwaggerBuilderExtensions.UseSwagger(app);

            app.UseSwaggerUI(options =>
            {
                options.DefaultModelRendering(ModelRendering.Model);
                options.DefaultModelExpandDepth(1);
                options.DisplayOperationId();
                options.DocExpansion(DocExpansion.None);
                options.DocumentTitle = "SMS Swagger";
                options.InjectStylesheet("/swagger-material.css");

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    options.RoutePrefix = string.Empty;
                }
            });

        }
    }
}
