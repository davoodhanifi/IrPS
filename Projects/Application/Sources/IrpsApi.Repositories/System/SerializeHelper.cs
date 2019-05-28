using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IrpsApi.Framework.System;
using Newtonsoft.Json;

namespace Noandishan.IrpsApi.Repositories.System
{
    public static class SerializeHelper
    {
        public static string SerializeParameters(this IEnumerable<ILogParameter> parameters)
        {
            var models = parameters.Select(p => p.ToLogParameterModel()).ToArray();

            var jsonSerilizeSettings = new JsonSerializerSettings();
            jsonSerilizeSettings.NullValueHandling = NullValueHandling.Ignore;
            
            return JsonConvert.SerializeObject(models, jsonSerilizeSettings);
        }

        public static IEnumerable<ILogParameter> DesrializeParameters(string json)
        {
            var jsonSerilizeSettings = new JsonSerializerSettings();
            jsonSerilizeSettings.NullValueHandling = NullValueHandling.Ignore;

            var models = JsonConvert.DeserializeObject<LogParameterModel[]>(json, jsonSerilizeSettings);
            return models.Select(m => m.ToLogParameter()).ToArray();
        }

        public static LogParameterModel ToLogParameterModel(this ILogParameter parameter)
        {
            var model = new LogParameterModel();
            model.Key = parameter.Key;

            if (parameter.Value == null)
                return model;

            if (parameter.Value is bool)
            {
                model.Type = "Boolean";
                model.Value = parameter.Value;
            }
            else if (parameter.Value is byte || parameter.Value is short || parameter.Value is ushort || parameter.Value is int || parameter.Value is uint || parameter.Value is long || parameter.Value is ulong)
            {
                model.Type = "Int64";
                model.Value = parameter.Value;
            }
            else if (parameter.Value is decimal || parameter.Value is float || parameter.Value is double)
            {
                model.Type = "Decimal";
                model.Value = parameter.Value;
            }
            else if (parameter.Value is string)
            {
                model.Type = "String";
                model.Value = parameter.Value;
            }
            else if (parameter.Value is DateTime)
            {
                model.Type = "DateTime";
                model.Value = ((DateTime)parameter.Value).ConvertToString();
            }
            else if (parameter.Value is byte[])
            {
                model.Type = "Binary";
                model.Value = Convert.ToBase64String((byte[])parameter.Value);
            }
            else
            {
                model.Type = "String";
                model.Value = parameter.Value.ToString();
            }

            return model;
        }

        public static ILogParameter ToLogParameter(this LogParameterModel parameterModel)
        {
            var parameter = new LogParameter();
            parameter.Key = parameterModel.Key;

            if (parameterModel.Value == null)
                return parameter;

            if (string.Equals(parameterModel.Type, "Boolean", StringComparison.OrdinalIgnoreCase))
                parameter.Value = Convert.ToBoolean(parameterModel.Value);
            else if (string.Equals(parameterModel.Type, "Int64", StringComparison.OrdinalIgnoreCase))
                parameter.Value = Convert.ToInt64(parameterModel.Value);
            else if (string.Equals(parameterModel.Type, "Decimal", StringComparison.OrdinalIgnoreCase))
                parameter.Value = Convert.ToDecimal(parameterModel.Value);
            else if (string.Equals(parameterModel.Type, "String", StringComparison.OrdinalIgnoreCase))
                parameter.Value = (string)parameterModel.Value;
            else if (string.Equals(parameterModel.Type, "DateTime", StringComparison.OrdinalIgnoreCase))
                parameter.Value = ((string)parameterModel.Value).ParseDateTime();
            else if (string.Equals(parameterModel.Type, "Binary", StringComparison.OrdinalIgnoreCase))
                parameter.Value = Convert.FromBase64String((string)parameterModel.Value);
            else
                throw new NotSupportedException();

            return parameter;
        }

        private static string ConvertToString(this DateTime dateTime)
        {
            var persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear(dateTime).ToString("0000");
            var month = persianCalendar.GetMonth(dateTime).ToString("00");
            var day = persianCalendar.GetDayOfMonth(dateTime).ToString("00");
            var hour = dateTime.Hour.ToString("00");
            var minute = dateTime.Minute.ToString("00");
            var second = dateTime.Second.ToString("00");

            return string.Concat(year, month, day, hour, minute, second);
        }

        private static DateTime ParseDateTime(this string stringData)
        {
            var year = 0;
            var month = 0;
            var day = 0;
            var hour = 0;
            var minute = 0;
            var second = 0;
            var miliSecond = 0;

            switch (stringData.Length)
            {
                case 8:
                    year = int.Parse(stringData.Substring(0, 4));
                    month = int.Parse(stringData.Substring(4, 2));
                    day = int.Parse(stringData.Substring(6, 2));
                    break;
                case 14:
                    year = int.Parse(stringData.Substring(0, 4));
                    month = int.Parse(stringData.Substring(4, 2));
                    day = int.Parse(stringData.Substring(6, 2));
                    hour = int.Parse(stringData.Substring(8, 2));
                    minute = int.Parse(stringData.Substring(10, 2));
                    second = int.Parse(stringData.Substring(12, 2));
                    break;
                case 17:
                    year = int.Parse(stringData.Substring(0, 4));
                    month = int.Parse(stringData.Substring(4, 2));
                    day = int.Parse(stringData.Substring(6, 2));
                    hour = int.Parse(stringData.Substring(8, 2));
                    minute = int.Parse(stringData.Substring(10, 2));
                    second = int.Parse(stringData.Substring(12, 2));
                    miliSecond = int.Parse(stringData.Substring(14, 3));
                    break;

                default:
                {
                    throw new Exception("Invalid datetime format");
                }
            }

            return new DateTime(year, month, day, hour, minute, second, miliSecond, new PersianCalendar());
        }
    }
}
