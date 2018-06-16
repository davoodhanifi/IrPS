using System;
using AutoMapper;
using IrpsApi.Api.ViewModels.Accounting;
using IrpsApi.Api.ViewModels.System;
using IrpsApi.Api.ViewModels.User;
using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.System;
using IrpsApi.Framework.User;

namespace IrpsApi.Api.Configurations
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMapping";

        public DomainToViewModelMappingProfile()
        {
            // Accounting
            CreateMap<IBalance, BalanceModel>();
            CreateMap<ITransaction, TransactionModel>();

            // System
            CreateMap<IOtp, OtpModel>();

            // User
            CreateMap<IUser, UserModel>();
        }
    }
}
