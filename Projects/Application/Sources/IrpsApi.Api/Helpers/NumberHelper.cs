using System;

namespace IrpsApi.Api.Helpers
{
    public static class NumberHelper
    {
        public static string ToPersianNumber(this string input)
        {
            var persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
                input = input.Replace(persian[j], j.ToString());

            return input;
        }
    }
}
