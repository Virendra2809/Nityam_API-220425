using HealthIndex.Business.Implementation;
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
    public class ConsultantDetailController : BaseAPIController
    {
        IConsultantDetailService  _consultantDetailService;

        public ConsultantDetailController(IConsultantDetailService consultantDetailService)
        {
            _consultantDetailService = consultantDetailService;
        }

        [HttpGet("GetConsutltantDetailsById")]
       [Authorize]
        [ProducesResponseType(typeof(ConsultantDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetConsutltantDetailsById(long consultantId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var ModelList = _consultantDetailService.GetConsutltantDetailsById(consultantId, ref errorResponseModel);

                if (ModelList != null)
                {
                    return Ok(ModelList);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpGet("GetConsultantDetail")]
        [Authorize]
        [ProducesResponseType(typeof(ConsultantDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetConsultantDetail([FromQuery]HealthParameter parameter)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var ModelList = _consultantDetailService.GetConsultantDetail(parameter,ref errorResponseModel);

                if (ModelList != null)
                {
                    return Ok(ModelList);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }

        }

        [HttpPost("AddConsultantDetail")]
        [Authorize]
        [ProducesResponseType(typeof(ConsultantDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddConsultantDetail([FromForm] ConsultantDetailModel model)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                MessageModel consultantmodel = _consultantDetailService.AddConsultantDetail(model, ref errorResponseModel);

                if (consultantmodel != null)
                {
                    var json = JsonConvert.SerializeObject(consultantmodel);
                    return Ok(json);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpGet("GetConsultingCategories")]
        [Authorize]
        [ProducesResponseType(typeof(ConsultantDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetConsultingCategories()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var ModelList = _consultantDetailService.GetConsultingCategories(ref errorResponseModel);

                if (ModelList != null)
                {
                    return Ok(ModelList);
                }
                var json = JsonConvert.SerializeObject(errorResponseModel);
                return Ok(json);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }

        }

        [HttpPut("EditConsultantDetails")]
        [Authorize]
        [ProducesResponseType(typeof(ConsultantDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult EditConsultantDetails([FromForm]ConsultantDetailModel consultantDetail)

        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            }

            if (consultantDetail == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                ErrorResponseModel errorResponseModel = null;

                var List = _consultantDetailService.EditConsultantDetails(consultantDetail);

                if (List != null)
                {
                    return Ok(List);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpDelete("DeleteConsultantDetails")]        
        [Authorize]
        [ProducesResponseType(typeof(ConsultantDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteConsultantDetails(int consultantId)
        {

            ErrorResponseModel errorResponseModel = null;
            try
            {
                var usermodel = _consultantDetailService.DeleteConsultantDetails(consultantId, ref errorResponseModel);

                if (usermodel != null)
                {
                    return Ok(usermodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpGet("GetExpertConsultantByCode")]
        [Authorize]
        [ProducesResponseType(typeof(ConsultantDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetExpertConsultantByCode(string consultantcode)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var ModelList = _consultantDetailService.GetExpertConsultantByCode(consultantcode, ref errorResponseModel);

                if (ModelList != null)
                {
                    return Ok(ModelList);
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