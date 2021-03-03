using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sms.Api.Core
{
    public class SmsSwagger
    {
        public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider provider;

            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
            {
                this.provider = provider;
            }

            public void Configure(SwaggerGenOptions options)
            {
                foreach (var description in this.provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        ConfigureSwaggerOptions.CreateInfoForApiVersion(description));
                }
            }

            private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
            {
                var info = new OpenApiInfo
                {
                    Title = "SMS API",
                    Contact = new OpenApiContact { Name = "Nigel Norman", Email = "iam.nigelnorman@gmail.com" },
                    Description = "SMS Application Programming Interface. Don't sue me, Oracle.",
                    Version = description.ApiVersion.ToString()
                };

                if (description.IsDeprecated)
                {
                    info.Description += " This API version has been deprecated.";
                }

                return info;
            }
        }

        internal class SwaggerDefaultValues : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                /*
                if (operation.Parameters == null)
                {
                    return;
                }

                foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
                {
                    var description = context.ApiDescription
                        .ParameterDescriptions
                        .First(p => p.Name == parameter.Name);

                    var routeInfo = description.RouteInfo;

                    if (parameter.Description == null)
                    {
                        parameter.Description = description.ModelMetadata?.Description;
                    }

                    if (routeInfo == null)
                    {
                        continue;
                    }

                    if (parameter.Default == null)
                    {
                        parameter.Default = routeInfo.DefaultValue;
                    }

                    parameter.Required |= !routeInfo.IsOptional;
                }
                */
                var apiDescription = context.ApiDescription;

                operation.deprecated = apiDescription.IsDeprecated();

                if (operation.parameters == null)
                {
                    return;
                }

                //foreach (var parameter in operation.parameters.OfType<NonBodyParameter>())
                //{
                //    var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                //    if (parameter.Description == null)
                //    {
                //        parameter.Description = description.ModelMetadata?.Description;
                //    }

                //    if (parameter.Default == null)
                //    {
                //        parameter.Default = description.DefaultValue;
                //    }

                //    parameter.Required |= description.IsRequired;
                //}
            }

            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                throw new NotImplementedException();
            }
        }

    }
}
