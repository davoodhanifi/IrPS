using System;
using System.Globalization;
using IrpsApi.Api.Models.Common;
using IrpsApi.Framework;

namespace IrpsApi.Api.Models
{
    internal static class SerializeHelper
    {
        public static string ConvertToString(this DateTime dateTime, bool includeTime = true)
        {
            var persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear(dateTime).ToString("0000");
            var month = persianCalendar.GetMonth(dateTime).ToString("00");
            var day = persianCalendar.GetDayOfMonth(dateTime).ToString("00");

            var date = year + month + day;
            if (!includeTime)
                return date;

            var hour = dateTime.Hour.ToString("00");
            var minute = dateTime.Minute.ToString("00");
            var second = dateTime.Second.ToString("00");

            return date + hour + minute + second;
        }

        public static string ConvertToString(this DateTime? dateTime, bool includeTime = true)
        {
            return dateTime?.ConvertToString(includeTime);
        }

        public static RecordMetadataModel ToRecordMetadataModel(this IRecordInfo recordInfo)
        {
            return new RecordMetadataModel
            {
                Version = recordInfo.Version,
                State = (Common.RecordState)(int)recordInfo.State,
                InsertDateTime = recordInfo.InsertDateTime.ConvertToString(true),
                UpdateDateTime = recordInfo.UpdateDateTime.ConvertToString(true),
                DeleteDateTime = recordInfo.DeleteDateTime.ConvertToString(true)
            };
        }

        public static EntityModel ToEntityModel(this IEntity entity)
        {
            return new EntityModel
            {
                Id = entity.Id
            };
        }

        public static RecordModel ToRecordModel(this IRecord record)
        {
            return new RecordModel
            {
                Id = record.Id,
                Meta = new RecordMetadataModel
                {
                    Version = record.RecordVersion,
                    State = (Common.RecordState)(int)record.RecordState,
                    InsertDateTime = record.RecordInsertDateTime.ConvertToString(true),
                    UpdateDateTime = record.RecordUpdateDateTime.ConvertToString(true),
                    DeleteDateTime = record.RecordDeleteDateTime.ConvertToString(true)
                }
            };
        }

        public static string GetObjectTypeName(this object obj)
        {
            if (obj is long)
                return "Int64";

            if (obj is decimal)
                return "Decimal";

            if (obj is bool)
                return "Boolean";

            if (obj is DateTime)
                return "DateTime";

            if (obj is string)
                return "String";

            if (obj is byte[])
                return "Binary";

            return null;
        }


        public static DateTime? ParseDateTime(string stringData)
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
                        return null;
                    }
            }

            return new DateTime(year, month, day, hour, minute, second, miliSecond, new PersianCalendar());
        }

        public static RecordMetadataModel GetRecordMetadataModel(this IRecord record)
        {
            return new RecordMetadataModel
            {
                Version = record.RecordVersion,
                State = (Common.RecordState)(int)record.RecordState,
                InsertDateTime = record.RecordInsertDateTime.ConvertToString(true),
                UpdateDateTime = record.RecordUpdateDateTime.ConvertToString(true),
                DeleteDateTime = record.RecordDeleteDateTime.ConvertToString(true)
            };
        }
    }
}
