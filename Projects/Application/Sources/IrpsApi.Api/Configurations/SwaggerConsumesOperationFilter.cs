using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IrpsApi.Api.Configurations
{
    public class SwaggerConsumesOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var contentTypes = new HashSet<string>();
            contentTypes.UnionWith(operation.Consumes);

            var consumesAttributes = context.ApiDescription.ActionAttributes().OfType<ConsumesAttribute>().ToList();
            if (consumesAttributes.Any())
            {
                foreach (var consumeAttr in consumesAttributes)
                {
                    foreach (var contentType in consumeAttr.ContentTypes)
                    {
                        contentTypes.Add(contentType);
                    }
                }
            }
            else
            {
                foreach (var parameter in context.ApiDescription.ParameterDescriptions)
                {
                    if (parameter.ParameterDescriptor is ControllerParameterDescriptor descriptor)
                    {
                        if (descriptor.ParameterInfo.GetCustomAttribute<FromFormAttribute>() != null)
                        {
                            contentTypes.Add("application/x-www-form-urlencoded");//multipart/form-data
                        }
                    }
                }
            }

            operation.Consumes = contentTypes.ToList();
        }
    }

}