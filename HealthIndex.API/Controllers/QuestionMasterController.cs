using HealthIndex.Business.Implementation;
using HealthIndex.Business.Interface;
using HealthIndex.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace HealthIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionMasterController : BaseAPIController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        IQuestionMasterSevice _questionmastersevice;

        public QuestionMasterController(IQuestionMasterSevice questionMasterSevice, IWebHostEnvironment webHostEnvironment)
        {
            _questionmastersevice = questionMasterSevice;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("GetQuestionSet")]
        [Authorize]
        [ProducesResponseType(typeof(QuestionMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetQuestionSet()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionmastermodelList = _questionmastersevice.GetQuestionSet(ref errorResponseModel);

                if (questionmastermodelList != null)
                {
                    return Ok(questionmastermodelList);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        [HttpGet("GetAllQuestions")]
       // [Authorize]
        [ProducesResponseType(typeof(QuestionlistModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllQuestions(long QuestionMasterId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var questionsModelList = _questionmastersevice.GetAllQuestions(QuestionMasterId,ref errorResponseModel);

                if (questionsModelList != null)
                {
                    return Ok(questionsModelList);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpPost("UploadQuestionImage")]
        //[Authorize]
        [ProducesResponseType(typeof(UploadImage), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult UploadQuestionImage([FromForm] UploadImage upload)
        {
            string webrootpath=_webHostEnvironment.WebRootPath;
            ErrorResponseModel errorResponseModel = null;  
            try
            {
                ResponseMessage appdataEntity = _questionmastersevice.UploadQuestionImage(upload);

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



       
    }
}
