using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IrpsApi.Api.Configurations
{
    public class SwaggerApiCategorizationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var path = context.ApiDescription.RelativePath;
            var namespaceIndex = path.IndexOf("/", StringComparison.OrdinalIgnoreCase);
            if (namespaceIndex < 0)
            {
                namespaceIndex = path.Length;
            }

            var segment = path.Substring(0, namespaceIndex).ToLower();
            if (segment != context.ApiDescription.GroupName)
            {
                operation.Tags = new List<string> { segment };
            }
        }
    }
}