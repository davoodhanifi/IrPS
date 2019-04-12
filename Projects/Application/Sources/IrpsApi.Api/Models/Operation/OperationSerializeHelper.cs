using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Operation;
using Mabna.WebApi.Common;

namespace IrpsApi.Api.Models.Operation
{
    internal class OperationMappingProfile : BaseMappingProfile
    {
        public OperationMappingProfile()
        {
            CreateMap<IRequestStatus, RequestStatusModel>();
            CreateMap<IRequestType, RequestTypeModel>();
            CreateMap<InputRequestModel, IRequest>();
            CreateMap<IRequest, RequestModel>()
                .ForPath(q => q.Account.Id, c => c.MapFrom(i => i.AccountId))
                .ForPath(q => q.Type.Id, c => c.MapFrom(i => i.TypeId))
                .ForPath(q => q.Status.Id, c => c.MapFrom(i => i.StatusId));
        }
    }

    internal static class OperationSerializeHelper
    {
        private static readonly IMapper Mapper;

        static OperationSerializeHelper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OperationMappingProfile>();
            });

            Mapper = config.CreateMapper();
        }

        public static IRequestType ToRequestType(this RequestTypeModel model)
        {
            return Mapper.Map<IRequestType>(model);
        }

        public static RequestTypeModel ToRequestTypeModel(this IRequestType requestType)
        {
            return Mapper.Map<RequestTypeModel>(requestType);
        }

        public static IRequestStatus ToRequestStatus(this RequestStatusModel model)
        {
            return Mapper.Map<IRequestStatus>(model);
        }

        public static RequestStatusModel ToRequestStatusModel(this IRequestStatus requestStatus)
        {
            return Mapper.Map<RequestStatusModel>(requestStatus);
        }

        public static RequestParameterModel ToRequestParameterModel(this IRequestParameter parameter)
        {
            var model = new RequestParameterModel();
            model.Key = parameter.Key;
            model.Type = parameter.Value.GetObjectTypeName();

            if (parameter.Value is DateTime)
            {
                model.Value = ((DateTime)parameter.Value).ConvertToString();
            }
            else if (parameter.Value is byte[])
            {
                model.Value = Convert.ToBase64String((byte[])parameter.Value);
            }
            else
            {
                model.Value = parameter.Value;
            }

            return model;
        }

        
        public static async Task<RequestModel> ToRequestModelAsync(this IRequest request, IExpandOptionCollection expandOptions, CancellationToken cancellationToken)
        {
            var model = Mapper.Map<RequestModel>(request);

            if (expandOptions.TryGetExpandOption<IAccount>("account", out var accountExpandOption))
            {
                var acc = await accountExpandOption.Engine.GetEntityAsync(request.AccountId, cancellationToken);
                model.Account = acc.ToAccountModel();
            }

            if (expandOptions.TryGetExpandOption<IRequestType>("type", out var typeExpandOption))
            {
                var type = await typeExpandOption.Engine.GetEntityAsync(request.TypeId, cancellationToken);
                model.Type = type.ToRequestTypeModel();
            }

            if (expandOptions.TryGetExpandOption<IRequestStatus>("status", out var statusExpandOption))
            {
                var status = await statusExpandOption.Engine.GetEntityAsync(request.StatusId, cancellationToken);
                model.Status = status.ToRequestStatusModel();
            }

            return model;
        }
    }
}
