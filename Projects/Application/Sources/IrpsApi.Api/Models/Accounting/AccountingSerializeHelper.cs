﻿using System;
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
                .ForPath(q => q.ToAccount.Id, c => c.MapFrom(iAccount => iAccount.ToAccountId))
                .ForPath(q => q.Type.Id, c => c.MapFrom(iType => iType.TypeId));

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
            //model.Id = transaction.Id;
            //model.Meta = transaction.GetRecordMetadataModel();

            //if (transaction.RecordState == Framework.RecordState.Deleted)
            //    return model;

            if (expandOptions.TryGetExpandOption<IAccount>("from_account", out var fromAccountExpandOption))
            {
                var acc = await fromAccountExpandOption.Engine.GetEntityAsync(transaction.FromAccountId, cancellationToken);
                model.FromAccount = acc.ToAccountModel();
            }

            if (expandOptions.TryGetExpandOption<IAccount>("to_account", out var toAccountExpandOption))
            {
                var acc = await toAccountExpandOption.Engine.GetEntityAsync(transaction.ToAccountId, cancellationToken);
                model.ToAccount = acc.ToAccountModel();
            }

            if (expandOptions.TryGetExpandOption<ITransactionType>("type", out var typeExpandOption))
            {
                var type = await typeExpandOption.Engine.GetEntityAsync(transaction.TypeId, cancellationToken);
                model.Type = type.ToTransactionTypeModel();
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

        public static TransactionTypeModel ToTransactionTypeModel(this ITransactionType transactionType)
        {
            return Mapper.Map<TransactionTypeModel>(transactionType);
        }
    }
}