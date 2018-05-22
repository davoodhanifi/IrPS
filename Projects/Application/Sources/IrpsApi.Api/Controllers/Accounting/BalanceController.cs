using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.ViewModels.User;
using IrpsApi.Framework.Accounting;
using IrpsApi.Framework.User;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers.Accounting
{
    [Route("api/[controller]")]
    public class BalanceController : ControllerBase<IUser>
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalanceController(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        [HttpGet("getbyusercode/{userCode}", Name = "GetBalanceByUserCode")]
        public async Task<IActionResult> GetByUserCode(string userCode, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userCode))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            try
            {
                var balance = await _balanceRepository.GetByUserCodeAsync(userCode, cancellationToken);
                if (balance == null)
                {
                    return NotFound();
                }

                return Ok(balance);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Error in processing");
            }
        }
    }
}