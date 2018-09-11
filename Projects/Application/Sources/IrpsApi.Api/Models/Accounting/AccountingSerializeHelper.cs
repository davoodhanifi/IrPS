using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.Accounts;
using Mabna.WebApi.Common;

namespace IrpsApi.Api.Models.Accounting
{
    internal class AccountingMappingProfile : BaseMappingProfile
    {
        public AccountingMappingProfile()
        {
            CreateMap<ITransaction, TransactionModel>()
                .ForPath(q => q.FromAccount.Id, c => c.MapFrom(iAccount => iAccount.FromAccountId))
                .ForPath(q => q.ToAccount.Id, c => c.MapFrom(iAccount => iAccount.ToAccountId));

            CreateMap<IBalance, BalanceModel>()
                .ForPath(q => q.Account.Id, c => c.MapFrom(iAccount => iAccount.AccountId));
        }
    }

    internal static class AccountingSerializeHelper
    {
        private static readonly IMapper Mapper;

        static AccountingSerializeHelper()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<AccountingMappingProfile>(); });

            Mapper = config.CreateMapper();
        }

        public static async Task<TransactionModel> ToTransactionModelAsync(this ITransaction transaction, IExpandOptionCollection expandOptions, CancellationToken cancellationToken = default)
        {
            var model = Mapper.Map<TransactionModel>(transaction);

            if (expandOptions.TryGetExpandOption<IAccount>("from_account", out var fromAccountExpandOption))
            {
                var acc = await fromAccountExpandOption.Engine.GetEntityAsync(transaction.FromAccountId, cancellationToken);
                model.FromAccount = acc.ToAccountModel();
            }

            if (expandOptions.TryGetExpandOption<IAccount>("to_account", out var toAccountExpandOption))
            {
                var acc = await toAccountExpandOption.Engine.GetEntityAsync(transaction.FromAccountId, cancellationToken);
                model.ToAccount = acc.ToAccountModel();
            }

            return model;
        }

        public static async Task<BalanceModel> ToBalanceModelAsync(this IBalance balance, IExpandOptionCollection expandOptions, CancellationToken cancellationToken = default)
        {
            var model = Mapper.Map<BalanceModel>(balance);

            if (expandOptions.TryGetExpandOption<IAccount>("account", out var accountExpandOption))
            {
                var acc = await accountExpandOption.Engine.GetEntityAsync(balance.AccountId, cancellationToken);
                model.Account = acc.ToAccountModel();
            }

            return model;
        }
    }
}