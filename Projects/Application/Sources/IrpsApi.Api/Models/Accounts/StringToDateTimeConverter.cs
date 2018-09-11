using System;
using AutoMapper;

namespace IrpsApi.Api.Models.Accounts
{
    public class StringToDateTimeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return SerializeHelper.ParseDateTime(source).Value;
        }
    }
}