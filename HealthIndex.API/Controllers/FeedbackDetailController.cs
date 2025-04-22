using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace HealthIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackDetailController : BaseAPIController
    {
        IFeedbackDetailService _IFeedbackDetailService;
        
        public FeedbackDetailController(IFeedbackDetailService feedbackdetailService)
        {
            _IFeedbackDetailService = feedbackdetailService;
        }


        [HttpGet]
      //  [Authorize]
        [ProducesResponseType(typeof(FeedbackDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetFeedbackDetails()
        {
            
            ErrorResponseModel errorResponseModel = null;
            try
            {
                
                var feedbackdetailModelList = _IFeedbackDetailService.GetFeedbackDetails(ref errorResponseModel);

                if (feedbackdetailModelList != null)
                {
                   return Ok(feedbackdetailModelList);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        [HttpPost("AddFeedback")]
       // [Authorize]
        [ProducesResponseType(typeof(FeedbackModel),200)]
        [ProducesResponseType(typeof(string),404)]
        [ProducesResponseType(typeof(string),400)]
        [ProducesResponseType(typeof(string),500)]
        public IActionResult AddFeedback(FeedbackModel model)
        {
         
            ErrorResponseModel errorResponseModel = null; 
            try
            {
                Message feedbackEntity = _IFeedbackDetailService.AddFeedback(model, ref errorResponseModel);

                if (feedbackEntity != null)
                {
                    var json = JsonConvert.SerializeObject(feedbackEntity);
                    return Ok(feedbackEntity);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        [HttpGet("GetFeedbackCount")]
       // [Authorize]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetFeedbackCount()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var feedbackModel = _IFeedbackDetailService.GetFeedbackCount(ref errorResponseModel);

                if (feedbackModel != null)
                {
                    return Ok(feedbackModel);
                }
                
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        [HttpGet("GetFeedbackById/{feedbackId}")]
        [ProducesResponseType(typeof(Feedbackcomment), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetFeedbackById(long feedbackId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var feedbackModel = _IFeedbackDetailService.GetFeedbackById(feedbackId, ref errorResponseModel);

                if (feedbackModel != null)
                {
                    return Ok(feedbackModel);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

    }
}
