using System;
using System.Globalization;

namespace Noandishan.IrpsApi.Repositories
{
    internal static class DateTimeHelper
    {
        public static string ConvertToString(this DateTime dateTime)
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

        public static DateTime ParseDateTime(this string stringData)
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
