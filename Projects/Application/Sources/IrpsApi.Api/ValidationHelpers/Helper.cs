using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;

namespace IrpsApi.Api.ValidationHelpers
{
    public static class ValidationHelpers
    {
        public static string GetDisplayName(this ValidationContext validationContext)
        {
            var paramName = validationContext.DisplayName;

            var dataMember = validationContext.ObjectType.GetProperty(validationContext.MemberName)?.GetCustomAttribute<DataMemberAttribute>();
            if (dataMember?.Name != null)
                paramName = dataMember.Name;

            return paramName;
        }
    }
}