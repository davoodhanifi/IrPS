using System.ComponentModel.DataAnnotations;
using IrpsApi.Api.Models;
using Mabna.WebApi.Common;

namespace IrpsApi.Api.ValidationHelpers
{
    public class InputDateTimeValidatorAttribute : ValidationAttribute
    {
        private readonly bool _nullable;

        public InputDateTimeValidatorAttribute(bool nullable = false)
        {
            _nullable = nullable;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                if (!_nullable)
                    throw new UnprocessableEntityException($"invalid_{validationContext.GetDisplayName()}", "value can not be null");

                return null;
            }

            try
            {
                var dateTime = SerializeHelper.ParseDateTime((string)value);
                if (dateTime == null)
                    throw new UnprocessableEntityException($"invalid_{validationContext.GetDisplayName()}", "value can not be null");
            }
            catch
            {
                throw new UnprocessableEntityException($"invalid_{validationContext.GetDisplayName()}", "value can not be null");
            }

            return ValidationResult.Success;
        }
    }
}