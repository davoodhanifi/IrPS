using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.ViewModels.User;
using IrpsApi.Framework.User;
using Microsoft.AspNetCore.Mvc;

namespace IrpsApi.Api.Controllers.User
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase<IUser>
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(Name = "GetByPhoneNumber")]
        public async Task<IActionResult> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            try
            {
                var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch
            {
                return StatusCode(500, "Error in processing");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserModel userModel, CancellationToken cancellationToken) 
        {
            if (userModel == null || userModel.PhoneNumber == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var user = await _userRepository.GetByPhoneNumberAsync(userModel.PhoneNumber, cancellationToken);
            if (user != null)
                throw new Exception("User Exists Already!");

            try
            {
                user = await _userRepository.CreateAsync(userModel, cancellationToken);
                return CreatedAtRoute("GetByPhoneNumber", new { phoneNumber = user.PhoneNumber }, userModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error in processing");
            }
        }
    }
}