using System;
using AutoMapper;
using IrpsApi.Api.Models.Accounts;

namespace IrpsApi.Api.Models
{
    public class BaseMappingProfile : Profile
    {
        public BaseMappingProfile()
        {
            CreateMap<string, DateTime>().ConvertUsing<StringToDateTimeConverter>();
        }
    }
}
