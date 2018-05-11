using System;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Framework.System;
using Mabna.WebApi.AspNetCore.Results;
using Mabna.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class OtpController : Controller
    {
        private readonly IOtpRepository _otpRepository;

        public OtpController(IOtpRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }

        [HttpGet("{id}", Name = "GetOtp")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var item = await _otpRepository.GetAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> Post(string phoneNumber, string deviceId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityResult(new ErrorModel());
            }

            try
            {
                var otp = await _otpRepository.CreateNewOtpAsync(phoneNumber, deviceId, cancellationToken);
                return CreatedAtRoute("GetOtp", new { id = otp.Id }, otp);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error in processing");
            }
        }
    }
}