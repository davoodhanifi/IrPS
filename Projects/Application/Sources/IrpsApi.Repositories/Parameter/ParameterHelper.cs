using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Noandishan.IrpsApi.Repositories.Parameter
{
    internal static class ParameterHelper
    {
        public static string SerializeParameters(this IEnumerable<ParameterModel> models)
        {
            var jsonSerilizeSettings = new JsonSerializerSettings();
            jsonSerilizeSettings.NullValueHandling = NullValueHandling.Ignore;

            return JsonConvert.SerializeObject(models, jsonSerilizeSettings);
        }

        public static IEnumerable<TParameter> DesrializeParameters<TParameter>(string json, Func<TParameter> parameterCreator) where TParameter : IParameter
        {
            var jsonSerilizeSettings = new JsonSerializerSettings();
            jsonSerilizeSettings.NullValueHandling = NullValueHandling.Ignore;

            var models = JsonConvert.DeserializeObject<ParameterModel[]>(json, jsonSerilizeSettings);
            return models.Select(m => parameterCreator().SetParameterValue(m.Key, m.Type, m.Value)).ToArray();
        }

        public static TParameter SetParameterValue<TParameter>(this TParameter parameter, string key, string type, object value) where TParameter : IParameter
        {
            parameter.Key = key;

            if (value == null)
                return parameter;

            if (string.Equals(type, "Boolean", StringComparison.OrdinalIgnoreCase))
                parameter.Value = Convert.ToBoolean(value);
            else if (string.Equals(type, "Int64", StringComparison.OrdinalIgnoreCase))
                parameter.Value = Convert.ToInt64(value);
            else if (string.Equals(type, "Decimal", StringComparison.OrdinalIgnoreCase))
                parameter.Value = Convert.ToDecimal(value);
            else if (string.Equals(type, "String", StringComparison.OrdinalIgnoreCase))
                parameter.Value = (string)value;
            else if (string.Equals(type, "DateTime", StringComparison.OrdinalIgnoreCase))
                parameter.Value = ((string)value).ParseDateTime();
            else if (string.Equals(type, "Binary", StringComparison.OrdinalIgnoreCase))
                parameter.Value = Convert.FromBase64String((string)value);
            else
                throw new NotSupportedException();

            return parameter;
        }

        public static ParameterModel CreateParameterModel(string key, object value)
        {
            var model = new ParameterModel();
            model.Key = key;

            if (value == null)
                return model;

            if (value is bool)
            {
                model.Type = "Boolean";
                model.Value = value;
            }
            else if (value is byte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong)
            {
                model.Type = "Int64";
                model.Value = value;
            }
            else if (value is decimal || value is float || value is double)
            {
                model.Type = "Decimal";
                model.Value = value;
            }
            else if (value is string)
            {
                model.Type = "String";
                model.Value = value;
            }
            else if (value is DateTime)
            {
                model.Type = "DateTime";
                model.Value = ((DateTime)value).ConvertToString();
            }
            else if (value is byte[])
            {
                model.Type = "Binary";
                model.Value = Convert.ToBase64String((byte[])value);
            }
            else
                throw new NotSupportedException();

            return model;
        }
    }
}
