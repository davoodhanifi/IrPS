using System.Collections.Generic;
using Mabna.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.ExpandOptionsHelpers
{
    [ModelBinder(typeof(ExpandOptionsModelBinder))]
    public class ExpandOptions
    {
        private readonly string[] _expandOptions;

        public ExpandOptions(string[] expandOptions)
        {
            _expandOptions = expandOptions;
        }

        public IExpandOptionCollection GetExpandOptions(ExpandEngineCollection expandEngineCollection, IEnumerable<string> validExpandOptions)
        {
            return ApiParametersHepler.ReadExpandOptions(_expandOptions, expandEngineCollection, validExpandOptions);
        }
    }
}