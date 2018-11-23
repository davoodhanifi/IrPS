using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IrpsApi.Api.ExpandOptionsHelpers
{
    public class ExpandOptionsModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(ExpandOptions))
                return Task.CompletedTask;

            if (!bindingContext.HttpContext.Request.Query.TryGetValue(bindingContext.ModelName, out var expands)) 
                return Task.CompletedTask;

            bindingContext.Model = new ExpandOptions(expands.ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);

            return Task.CompletedTask;
        }
    }
}