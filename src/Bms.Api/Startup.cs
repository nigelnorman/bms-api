using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bms.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Bms.Api.Services.MapperConfigs;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Bms.Api.Core.BmsSwagger;

namespace Bms.Api
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
            services.AddDbContext<BmsDbContext>(options =>
            {
                var connection = this.Configuration.GetConnectionString(nameof(BmsDbContext));
                //options.useSqlServer(connection, opts => opts.MigrationsAssembly("PropLogix.Mosaic.Data"));
                //this.Logger.LogDebug("{@SqlOptions}", options);
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)                       
                .AddJsonOptions(options =>
                                {
                                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                                    options.SerializerSettings.Converters.Add(new StringEnumConverter
                                        { NamingStrategy = new CamelCaseNamingStrategy() });
                                });

            AddSwagger(services);

#pragma warning disable CS0618 // Type or member is obsolete
            services.AddAutoMapper();
#pragma warning restore CS0618 // Type or member is obsolete
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

            services.AddSwaggerGen(options =>
            {
                // NOTE: This is required so that we can map the FileStreamResult to the correct
                // client-side setting.
                options.MapType<FileStreamResult>(() => new Schema
                {
                    Type = "file"
                });

                //options.OperationFilter<SwaggerDefaultValues>();

                //options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "JWT Auth using Bearer token in Authorization header",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});

                //options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //    {"Bearer", Array.Empty<string>()}
                //});

                options.DescribeAllEnumsAsStrings();
                options.UseReferencedDefinitionsForEnums();

                //options.IncludeXmlComments(MosaicSwagger.XmlCommentsFilePath());

            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        }

        public void UseSwagger(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            SwaggerBuilderExtensions.UseSwagger(app);

            app.UseSwaggerUI(options =>
            {
                options.DefaultModelRendering(ModelRendering.Model);
                options.DefaultModelExpandDepth(1);
                options.DisplayOperationId();
                options.DocExpansion(DocExpansion.None);
                options.DocumentTitle = "BMS Swagger";
                options.InjectStylesheet("/swagger-material.css");

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

        }
    }
}
