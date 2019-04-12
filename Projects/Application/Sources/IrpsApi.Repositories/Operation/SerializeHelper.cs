using System;
using System.Collections.Generic;
using System.Linq;
using IrpsApi.Framework.Operation;
using Noandishan.IrpsApi.Repositories.Parameter;

namespace Noandishan.IrpsApi.Repositories.Operation
{
    internal static class SerializeHelper
    {
        public static string SerializeParameters(this IEnumerable<IRequestParameter> parameters)
        {
            return parameters?.Select(p => ParameterHelper.CreateParameterModel(p.Key, p.Value)).SerializeParameters();
        }

        public static IEnumerable<IRequestParameter> DesrializeParameters(string json)
        {
            return ParameterHelper.DesrializeParameters(json, () => new RequestParameter());
        }

        public static IRequestParameter CreateRequestParameter(string key, string type, object value)
        {
            return new RequestParameter().SetParameterValue(key, type, value);
        }
    }
}
