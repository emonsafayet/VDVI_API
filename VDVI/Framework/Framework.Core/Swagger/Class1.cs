using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Framework.Core.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IConfiguration _config;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configProvider)
        {
            _provider = provider;
            _config = configProvider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription versionDescription in (IEnumerable<ApiVersionDescription>)_provider.ApiVersionDescriptions)
                options.SwaggerDoc(versionDescription.GroupName, CreateInfoForApiVersion(versionDescription));
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            OpenApiInfo openApiInfo = new OpenApiInfo()
            {
                Version = description.ApiVersion.ToString(),
                Title = _config["swagger:title"],
                Description = _config["swagger:description"]
            };
            if (description.IsDeprecated)
                openApiInfo.Description += string.IsNullOrEmpty(_config["swagger:deprecatedMsg"]) ? " This API version has been deprecated." : _config["swagger:deprecatedMsg"];

            return openApiInfo;
        }
    }
}
