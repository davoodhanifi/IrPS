using System.ComponentModel.DataAnnotations;
using Mabna.WebApi.Common;

namespace IrpsApi.Api.ValidationHelpers
{
    public class Required : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                var paramName = validationContext.GetDisplayName();
                throw new UnprocessableEntityException($"missing_{paramName}", $"parameter {paramName} is required.", paramName);
            }

            return ValidationResult.Success;
        }
    }
}