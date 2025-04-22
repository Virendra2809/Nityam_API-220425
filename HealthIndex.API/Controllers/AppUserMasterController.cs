using HealthIndex.Business.Implementation;
using HealthIndex.Business.Interface;
using HealthIndex.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System;
using HealthIndex.Common;
using FirebaseAdmin.Messaging;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using HealthIndex.Entity.DataModels;
using Microsoft.Recognizers.Text;

namespace HealthIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserMasterController : BaseAPIController
    {
        IAppUserMasterService _appUserMasterService;
        Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly string filePath;
        HealthIndexDbContext _healthindexdbcontext;

        public AppUserMasterController(IAppUserMasterService appUserMasterService, HealthIndexDbContext healthindexdbcontext, Microsoft.Extensions.Configuration.IConfiguration configuration, string filePath)
        {
            _appUserMasterService = appUserMasterService;
            _healthindexdbcontext = healthindexdbcontext;

            _configuration = configuration;
            this.filePath = filePath;
        }


        [HttpPost("AuthenticateUserForMobile")]
       // [Authorize]
        public IActionResult AuthenticateUserForMobile(LoginModelForUser model)
        {
            try
            {
                DateTime ExpDate = DateTime.Now.Date;
                DateTime TodayDate = DateTime.Now.Date;                
                int daysRemaining;
                ErrorResponseModel errorResponseModel = null;
                if (!ModelState.IsValid)
                {
                    var errorMessage = string.Join(",", ModelState.Values.ToList());
                    return BadRequest(new { message = errorMessage });
                }
                var authData = _appUserMasterService.AuthenticateUserForMobile(model.Email, model.Password, ref errorResponseModel);
                if (authData != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, authData.AppUserId.ToString()),
                        //new Claim(ClaimTypes.Role, authData.Role),
                    };

                 ////   DateTime expDate = DateTime.ParseExact(authData.ExpiryDate, "ddMMyy", System.Globalization.CultureInfo.InvariantCulture);

                 //   // Format the date as "ddMMyyyy"
                 //   string formattedExpDate = ExpDate.ToString("ddMMyyyy");
                 //   string currentDate = DateTime.Now.ToString("ddMMyyyy");

                 //   TimeSpan remainingDays = ExpDate - DateTime.Now;
                 //   authData.daysRemaining = remainingDays.Days;




                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        authData.AppUserId,
                        authData.FirstName,
                        authData.LastName,
                        authData.EmailId,
                        authData.MobileNo,
                        authData.Guid,
                        authData.IsActive,
                        authData.UserPhoto,
                        authData.ExpiryDate,
                        authData.daysRemaining,
                        authData.Name,
                        authData.Validity,
                        authData.OrderNo,
                        authData.OrderStatus
                    });
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("ForgotPasswordLinkForMobile")]
        public IActionResult ForgotPasswordLinkForMobile(string email)
        {
            ErrorResponseModel errorResponseModel = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                MessageModel message = _appUserMasterService.ForgotPasswordLinkForMobile(email, ref errorResponseModel);
                var json = JsonConvert.SerializeObject(message);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        [HttpPost("AddAppUser")]
        [ProducesResponseType(typeof(AppUserMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddAppUser(AppUserMasterModel model)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var isDuplicateEmail = _healthindexdbcontext.AppUserMasters.Where(x => (x.EmailId) == model.EmailId).FirstOrDefault();
                MessageModel appusermodel = new MessageModel();
             

                    if (isDuplicateEmail == null)
                    {
                        appusermodel = _appUserMasterService.AddAppUser(model, ref errorResponseModel);

                        if (appusermodel != null)
                        {
                            var json = JsonConvert.SerializeObject(appusermodel);
                            return Ok(json);
                        }
                        return ReturnErrorResponse(errorResponseModel);

                    }
                    else
                    {
                        appusermodel.message = "Duplicate Email";
                        appusermodel.StatusCode = 406;
                        var json = JsonConvert.SerializeObject(appusermodel);
                        return Ok(json);//  Ok(json);
                    }
                
              


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }



        [HttpPut("UpdateAppUser")]
        [ProducesResponseType(typeof(AppUserMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult UpdateAppUser(AppUserMasterModel model)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                MessageModel appusermodel = new MessageModel();

                var isDuplicateEmail = _healthindexdbcontext.AppUserMasters.FirstOrDefault(x =>
        x.AppUserId != model.AppUserId && x.EmailId == model.EmailId);


                if (isDuplicateEmail == null)
                {
                    appusermodel = _appUserMasterService.UpdateAppUser(model, ref errorResponseModel);

                    if (appusermodel != null)
                    {
                        var json = JsonConvert.SerializeObject(appusermodel);
                        return Ok(json);
                    }
                    return ReturnErrorResponse(errorResponseModel);
                }

                else
                {
                    appusermodel.message = "Duplicate Email";
                    appusermodel.StatusCode = 406;
                    var json = JsonConvert.SerializeObject(appusermodel);
                    return Ok(json);//  Ok(json);
                }



               

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        [HttpPost("EditSelfAssessment")]
        [ProducesResponseType(typeof(QuestionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult EditSelfAssessment(QuestionModel model)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                MessageModel materiamedicheadmodel = _appUserMasterService.EditSelfAssessment(model, ref errorResponseModel);

                if (materiamedicheadmodel != null)
                {
                    var json = JsonConvert.SerializeObject(materiamedicheadmodel);
                    return Ok(json);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        [HttpGet("GetFeedbackDetailsForMobile")]
        [Authorize]
        [ProducesResponseType(typeof(FeedbackListModel1), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetFeedbackDetailsForMobile(long AppUserId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var feedbackdetailList = _appUserMasterService.GetFeedbackDetailsForMobile(AppUserId, ref errorResponseModel);

                if (feedbackdetailList != null)
                {
                    return Ok(feedbackdetailList);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        [HttpGet("ActivateUserCount")]
        [Authorize]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult ActivateUserCount()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var activeModel = _appUserMasterService.ActivateUserCount(ref errorResponseModel);

                if (activeModel != null)
                {
                    return Ok(activeModel);
                }

                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }



        [HttpGet("InActiveUserCount")]
        [Authorize]       
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult InActiveUserCount()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var inactiveModel = _appUserMasterService.InActiveUserCount(ref errorResponseModel);

                if (inactiveModel != null)
                {
                    return Ok(inactiveModel);
                }

                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }



        [HttpPost("EditAppUser")]
        
        public IActionResult EditAppUser(AppUser model)
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
                MessageModel appuserModel = _appUserMasterService.EditAppUser(model, ref errorMessage);
                if (appuserModel != null)
                {
                    var json = JsonConvert.SerializeObject(appuserModel);
                    return Ok(json);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }



        [HttpDelete]
        [Route("DeleteAppUser")]
        [Authorize]
        [ProducesResponseType(typeof(AppUserMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteAppUser(long  appuserId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                MessageModel appusermodel = _appUserMasterService.DeleteAppUser(appuserId, ref errorResponseModel);

                if (appusermodel != null)
                {
                    var json = JsonConvert.SerializeObject(appusermodel);
                    return Ok(json);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpPost("UploadPhoto")]
        //[Authorize]
        [ProducesResponseType(typeof(UploadPhoto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult UploadPhoto([FromForm] UploadPhoto upload)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
              ResponseMessage appdataEntity = _appUserMasterService.UploadPhoto(upload);

                if (appdataEntity != null)
                {
                    var json = JsonConvert.SerializeObject(appdataEntity);
                    return Ok(json);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        [HttpPost("ChangePasswordForMobile")]
        [ProducesResponseType(typeof(ChangePassword), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult ChangePasswordForMobile(ChangePassword password)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                ResponseMessage UserModel = _appUserMasterService.ChangePasswordForMobile(password, ref errorResponseModel);

                if (UserModel != null)
                {
                    var json = JsonConvert.SerializeObject(UserModel);
                    return Ok(json);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

    }
}
