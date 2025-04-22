using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseAPIController
    {
        IAuthService _authService;
        IUserService _userService;
        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;

        }

        [HttpGet]
       // [Authorize]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllUser()
        {
            var errorMessage = new ErrorResponseModel();
            try
            {
                var model = new UserModel();
                var productModel = _userService.GetAllUser();
                if (productModel != null)
                {
                    return Ok(productModel);
                }
                errorMessage.Message = GlobalConstants.NotFoundMessage;
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// To get user by User ID 
        /// </summary>       
        /// <returns></returns>
        [HttpGet("GetUserById")]
       // [Authorize]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetUserById()
        {

            ErrorResponseModel errorResponseModel = null;
            try
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;

                    var userModel = _userService.GetUserById(Convert.ToInt32(userId), ref errorResponseModel);
                   
                    if (userModel != null)
                    {
                        return Ok(userModel);
                    }
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Post([FromForm]UserModel model)
        {

            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
               // var emailSettings = _authService.EmailLogin

                var userModel = _userService.AddUser(model, ref errorMessage);
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
