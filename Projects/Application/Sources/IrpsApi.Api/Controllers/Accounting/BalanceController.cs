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
        private readonly IUserRepository _userRepository;

        public BalanceController(IBalanceRepository balanceRepository, IUserRepository userRepository)
        {
            _balanceRepository = balanceRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get user credit by user code.
        /// </summary>
        /// <param name="user_code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Last credit of user.</returns>
        /// <response code="422">Invalid modelState.</response>
        /// <response code="400">User code is null or empty.</response>
        /// <response code="404"></response>   
        /// <response code="404">User not found; Balance for this user not found.</response>
        /// <response code="200">Successfully return current user credit.</response>
        [HttpGet("getbyusercode/{user_code}", Name = "GetBalanceByUserCode")]
        [ProducesResponseType(422)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetByUserCode(string user_code, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (string.IsNullOrEmpty(user_code))
            {
                return BadRequest("User Code Not Defined!");
            }

            try
            {
                var user = await _userRepository.GetByUserCodeAsync(user_code, cancellationToken);
                if (user == null)
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "User Not Found!");

                var balance = await _balanceRepository.GetByUserCodeAsync(user_code, cancellationToken);
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