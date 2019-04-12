using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.Models.Operation;
using IrpsApi.Framework.Operation.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers.Operation
{
    public class RequestTypeController : RequiresAuthenticationApiControllerBase
    {
        private readonly IRequestTypeRepository _requestTypeRepository;

        public RequestTypeController(IRequestTypeRepository requestTypeRepository)
        {
            _requestTypeRepository = requestTypeRepository;
        }

        /// <summary>
        /// Get request types.
        /// </summary>
        [HttpGet]
        [Route("operation/requesttypes")]
        public async Task<ActionResult<IEnumerable<RequestTypeModel>>> GetRequestTypesAsync(CancellationToken cancellationToken)
        {
            var types = await _requestTypeRepository.GetAllRequestTypesAsync(cancellationToken);
            return Ok(types.Select(item => item.ToRequestTypeModel()).ToList());
        }
    }
}
