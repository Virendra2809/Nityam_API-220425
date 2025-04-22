using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace HealthIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserModelController : BaseAPIController
    {
        IUserModelService _userService;
        private readonly IWebHostEnvironment iwebhostingEnvironment;
        private readonly string filePath;

        private readonly IOptions<SmtpSettingModel> _emailSettings;
        public UserModelController(IUserModelService userService, IOptions<SmtpSettingModel> emailSettings, string filePath, IWebHostEnvironment iwebhostingEnvironment)
        {
            _userService = userService;
            _emailSettings = emailSettings;
            this.iwebhostingEnvironment = iwebhostingEnvironment;
            this.filePath = filePath;

        }

        [HttpGet("GetRegisteredUser")]
        [Authorize]
        [ProducesResponseType(typeof(AppUserListModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetRegisteredUser()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var userModel = _userService.GetRegisteredUser();

                if (userModel != null)
                {
                    return Ok(userModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        [HttpPost("ForgotPasswordLink")]
        public IActionResult ForgotPasswordLink(string email)
        {
            ErrorResponseModel errorResponseModel = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                string message = _userService.ForgotPasswordLink(email, ref errorResponseModel);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpGet("GetAdminUserDetailsById")]
        [Authorize]
        [ProducesResponseType(typeof(AdminUserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAdminUserDetailsById(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var adminuserModel = _userService.GetAdminUserDetailsById(userId,ref  errorResponseModel);

                if (adminuserModel != null)
                {
                    return Ok(adminuserModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        [HttpGet("GetCount")]
        [Authorize]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetCount()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var userModel = _userService.GetCount(ref errorResponseModel);

                if (userModel != null)
                {
                    return Ok(userModel);
                }

                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

       
        [HttpPost("EditAdminUsers")]
        [Authorize]
        public IActionResult EditAdminUsers(AdminUserModel model)
        {

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            }

            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var userModel = _userService.EditAdminUsers(model, ref errorMessage);
                if (!string.IsNullOrEmpty(userModel))
                {
                    return Ok(userModel);
                }
             
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


    }
}
