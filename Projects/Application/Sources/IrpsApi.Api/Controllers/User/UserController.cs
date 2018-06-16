using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpGet("getbyphonenumber/{phoneNumber}", Name = "GetByPhoneNumber")]
        public async Task<IActionResult> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken)
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
                var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);
                if (user == null)
                {
                    return NotFound("Not Found User By This Phone Number");
                }

                var userViewModel = Mapper.Map<UserModel>(user);
                return Ok(userViewModel);
            }
            catch
            {
                return StatusCode(500, "Error In Processing");
            }
        }

        [HttpGet("getbyusercode/{userCode}", Name = "GetByUserCode")]
        public async Task<IActionResult> GetByUserCode(string userCode, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (string.IsNullOrEmpty(userCode))
            {
                return BadRequest("User Codes Not Defined!");
            }

            try
            {
                var user = await _userRepository.GetByUserCodeAsync(userCode, cancellationToken);
                if (user == null)
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, "User Not Found!");

                var userViewModel = Mapper.Map<UserModel>(user);
                return Ok(userViewModel);
            }
            catch
            {
                return StatusCode(500, "Error In Processing");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserModel userModel, CancellationToken cancellationToken) 
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if (userModel?.PhoneNumber == null)
            {
                return BadRequest("Phone Number Not Defined!");
            }
           
            try
            {
                var user = await _userRepository.GetByPhoneNumberAsync(userModel.PhoneNumber, cancellationToken);
                if (user != null)
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity, "User Exist Already!");

                user = await _userRepository.CreateAsync(userModel, cancellationToken);
                var userViewModel = Mapper.Map<UserModel>(user);
                return CreatedAtRoute("GetByPhoneNumber", new { phoneNumber = userViewModel.PhoneNumber }, userViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error In Processing");
            }
        }
    }
}