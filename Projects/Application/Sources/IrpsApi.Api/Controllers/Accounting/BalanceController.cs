using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IrpsApi.Api.ViewModels.Accounting;
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
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (string.IsNullOrEmpty(userCode))
            {
                return BadRequest("User Code Not Defined!");
            }

            try
            {
                var balance = await _balanceRepository.GetByUserCodeAsync(userCode, cancellationToken);
                if (balance == null)
                {
                    return NotFound();
                }

                var balanceViewModel = Mapper.Map<BalanceModel>(balance);
                return Ok(balanceViewModel);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Error In Processing");
            }
        }
    }
}