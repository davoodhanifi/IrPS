using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.Models.Operation;
using IrpsApi.Framework.Operation.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers.Operation
{
    public class RequestStatusController : RequiresAuthenticationApiControllerBase
    {
        private readonly IRequestStatusRepository _requestStatusRepository;

        public RequestStatusController(IRequestStatusRepository requestStatusRepository)
        {
            _requestStatusRepository = requestStatusRepository;
        }

        /// <summary>
        /// Get request Statuss.
        /// </summary>
        [HttpGet]
        [Route("operation/requestStatuses")]
        public async Task<ActionResult<IEnumerable<RequestStatusModel>>> GetRequestStatusesAsync(CancellationToken cancellationToken)
        {
            var statuses = await _requestStatusRepository.GetAllRequestStatusesAsync(cancellationToken);
            return Ok(statuses.Select(item => item.ToRequestStatusModel()).ToList());
        }
    }
}
