using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IrpsApi.Api.ViewModels.System;
using IrpsApi.Framework.System;
using IrpsApi.Framework.User;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers.System
{
    [Route("api/[controller]")]
    public class OtpController : ControllerBase<IOtp>
    {
        private readonly IOtpRepository _otpRepository;
        private readonly IUserRepository _userRepository;

        public OtpController(IOtpRepository otpRepository, IUserRepository userRepository)
        {
            _otpRepository = otpRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{id}", Name = "GetOtp")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (id <= 0)
            {
                return BadRequest("Bad Id!");
            }

            var item = await _otpRepository.GetAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound("Item Not Exist!");
            }

            var itemViewModel = Mapper.Map<OtpModel>(item);
            return Ok(itemViewModel);
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckOtp(string phoneNumber, string password, CancellationToken cancellationToken)
        {
            var otp = await _otpRepository.CheckAsync(phoneNumber, password, cancellationToken);
            if (otp == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);
            if (user == null)
            {
                return new OkObjectResult(KeyValuePair.Create("is_registered", false));
            }

            return new OkObjectResult(KeyValuePair.Create("is_registered", true));
        }


        [HttpPost]
        public async Task<IActionResult> Post(string phoneNumber, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Phone Number Not Defined!");
            }

            try
            {
                _otpRepository.DeleteAsync(cancellationToken);

                var otp = await _otpRepository.CreateAsync(phoneNumber, cancellationToken);
                var otpViewModel = Mapper.Map<OtpModel>(otp);
                return CreatedAtRoute("GetOtp", new { id = otpViewModel.Id }, otpViewModel);
            }
            catch
            {
                return StatusCode(500, "Error In Processing");
            }
        }
    }
}