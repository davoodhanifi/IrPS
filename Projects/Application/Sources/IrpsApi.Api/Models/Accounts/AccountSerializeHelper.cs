using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Framework.Accounts;
using Mabna.WebApi.Common;

namespace IrpsApi.Api.Models.Accounts
{
    internal class AccountsMappingProfile : BaseMappingProfile
    {
        public AccountsMappingProfile()
        {
            CreateMap<IAccount, AccountModel>();
            CreateMap<ISession, SessionModel>()
                .ForPath(q => q.State.Id, c => c.MapFrom(isession => isession.StateId))
                .ForPath(q => q.Account.Id, c => c.MapFrom(iSession => iSession.AccountId));
            CreateMap<IPersonProfile, PersonProfileModel>()
                .ForPath(q => q.Account.Id, c => c.MapFrom(i => i.AccountId));
            CreateMap<InputProfileModel, IPersonProfile>();
        }
    }

    internal static class AccountSerializeHelper
    {
        private static readonly IMapper Mapper;

        static AccountSerializeHelper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountsMappingProfile>();
            });

            Mapper = config.CreateMapper();
        }

        public static AccountModel ToAccountModel(this IAccount account)
        {
            return Mapper.Map<AccountModel>(account);
        }

        public static async Task<SessionModel> ToSessionModelAsync(this ISession session, IExpandOptionCollection expandOptions, CancellationToken cancellationToken = default)
        {
            var model = Mapper.Map<SessionModel>(session);

            if (expandOptions.TryGetExpandOption<IAccount>("account", out var accountExpandOption))
            {
                var acc = await accountExpandOption.Engine.GetEntityAsync(session.AccountId, cancellationToken);
                model.Account = acc.ToAccountModel();
            }

            return model;
        }

        public static async Task<PersonProfileModel> ToPersonProfileModelAsync(this IPersonProfile profile, IExpandOptionCollection expandOptions, CancellationToken cancellationToken = default)
        {
            var model = Mapper.Map<PersonProfileModel>(profile);

            if (expandOptions.TryGetExpandOption<IAccount>("account", out var accountExpandOption))
            {
                var acc = await accountExpandOption.Engine.GetEntityAsync(profile.AccountId, cancellationToken);
                model.Account = acc.ToAccountModel();
            }

            return model;
        }

        public static IPersonProfile ToPersonProfile(this InputProfileModel model)
        {
            return Mapper.Map<IPersonProfile>(model);
        }
    }
}