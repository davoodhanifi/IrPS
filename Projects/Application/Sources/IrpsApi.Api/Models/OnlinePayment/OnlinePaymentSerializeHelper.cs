using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Framework.OnlinePayment;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Framework.Accounts;
using Mabna.WebApi.Common;

namespace IrpsApi.Api.Models.OnlinePayment
{
    internal class OnlinePaymentMappingProfile : BaseMappingProfile
    {
        public OnlinePaymentMappingProfile()
        {
            CreateMap<IOnlinePaymentGateway, OnlinePaymentGatewayModel>();
            CreateMap<IOnlinePaymentParameter, OnlinePaymentParameterModel>();
            CreateMap<IOnlinePaymentState, OnlinePaymentStateModel>();
            CreateMap<InputOnlinePaymentModel, IOnlinePayment>();
            CreateMap<IOnlinePayment, OnlinePaymentModel>()
                .ForPath(q => q.Account.Id, c => c.MapFrom(iSession => iSession.AccountId))
                .ForPath(q => q.Gateway.Id, c => c.MapFrom(iGetway => iGetway.Id))
                .ForPath(q => q.State.Id, c => c.MapFrom(iState => iState.Id));
        }
    }

    internal static class OnlinePaymentSerializeHelper
    {
        private static readonly IMapper Mapper;

        static OnlinePaymentSerializeHelper()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<OnlinePaymentMappingProfile>(); });

            Mapper = config.CreateMapper();
        }

        public static OnlinePaymentParameterModel ToOnlinePaymentParameterModel(this IOnlinePaymentParameter parameter, IExpandOptionCollection expandOptions)
        {
            return Mapper.Map<OnlinePaymentParameterModel>(parameter);
        }

        public static OnlinePaymentStateModel ToOnlinePaymentStateModel(this IOnlinePaymentState onlinePaymentState)
        {
            return Mapper.Map<OnlinePaymentStateModel>(onlinePaymentState);
        }

        public static OnlinePaymentGatewayModel ToOnlinePaymentGatewayModel(this IOnlinePaymentGateway onlinePaymentGateway)
        {
            return Mapper.Map<OnlinePaymentGatewayModel>(onlinePaymentGateway);
        }

        public static async Task<OnlinePaymentModel> ToOnlinePaymentModelAsync(this IOnlinePayment onlinePayment, IExpandOptionCollection expandOptions, CancellationToken cancellationToken)
        {
            var model = Mapper.Map<OnlinePaymentModel>(onlinePayment);

            if (expandOptions.TryGetExpandOption<IAccount>("account", out var accountExpandOption))
            {
                var acc = await accountExpandOption.Engine.GetEntityAsync(onlinePayment.AccountId, cancellationToken);
                model.Account = acc.ToAccountModel();
            }

            if (onlinePayment.OnlinePaymentParameters != null && onlinePayment.OnlinePaymentParameters.Any())
            {
                var expandOption = expandOptions?.GetOption("parameters");
                if (expandOption != null)
                {
                    model.OnlinePaymentParameters = onlinePayment.OnlinePaymentParameters.Select(t => t.ToOnlinePaymentParameterModel(expandOptions));
                }
                else
                {
                    model.OnlinePaymentParameters = onlinePayment.OnlinePaymentParameters.Select(t => new OnlinePaymentParameterModel
                    {
                        Id = t.Id
                    });
                }
            }

            return model;
        }

        public static async Task<OnlinePaymentInitResultModel> ToOnlinePaymentInitResultModel(this IOnlinePaymentInitResult result, IExpandOptionCollection expandOptions, CancellationToken cancellationToken)
        {
            var model = new OnlinePaymentInitResultModel();
            model.GatewayUrl = result.GatewayUrl;
            model.PaymentUrl = result.PaymentUrl;
            model.Parameters = result.Parameters;

            var expandOption = expandOptions?.GetOption("online_payment");
            if (expandOption == null)
                model.OnlinePayment = new OnlinePaymentModel
                {
                    Id = result.OnlinePayment.Id
                };
            else
                model.OnlinePayment = await result.OnlinePayment.ToOnlinePaymentModelAsync(expandOption.ChildExpandOptions, cancellationToken);

            return model;
        }
    }
}