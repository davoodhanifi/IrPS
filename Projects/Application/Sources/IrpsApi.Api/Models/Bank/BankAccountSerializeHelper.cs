using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Bank;
using Mabna.WebApi.Common;

namespace IrpsApi.Api.Models.Bank
{
    internal class BankAccountsMappingProfile : BaseMappingProfile
    {
        public BankAccountsMappingProfile()
        {
            CreateMap<IBank, BankModel>();
            CreateMap<InputBankAccountModel, IBankAccount>();
            CreateMap<IBankAccount, BankAccountModel>()
                .ForPath(q => q.Account.Id, c => c.MapFrom(iSession => iSession.AccountId));
        }
    }

    internal static class BankAccountSerializeHelper
    {
        private static readonly IMapper Mapper;

        static BankAccountSerializeHelper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BankAccountsMappingProfile>();
            });

            Mapper = config.CreateMapper();
        }

        public static async Task<BankAccountModel> ToBankAccountModelAsync(this IBankAccount profile, IExpandOptionCollection expandOptions, CancellationToken cancellationToken = default)
        {
            var model = Mapper.Map<BankAccountModel>(profile);

            if (expandOptions.TryGetExpandOption<IAccount>("account", out var accountExpandOption))
            {
                var acc = await accountExpandOption.Engine.GetEntityAsync(profile.AccountId, cancellationToken);
                model.Account = acc.ToAccountModel();
            }

            return model;
        }

        public static IBankAccount ToBankAccount(this InputBankAccountModel model)
        {
            return Mapper.Map<IBankAccount>(model);
        }
    }
}