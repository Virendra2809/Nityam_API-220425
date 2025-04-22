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
    public class FirmDetailController : BaseAPIController
    {
        IFirmDetail _firmdetailService;
        public FirmDetailController(IFirmDetail firmdetailService)
        {
            _firmdetailService = firmdetailService;

        }

        [HttpGet]
       // [Authorize]
        [ProducesResponseType(typeof(FirmdetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllFirm()
        {
            var errorMessage = new ErrorResponseModel();
            try
            {
                var model = new FirmdetailModel();
                var productModel = _firmdetailService.GetAllFirm();
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
        [HttpGet("{FirmId}")]
        //[Authorize]
        [ProducesResponseType(typeof(FirmdetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetUById(long FirmId)
        {

            ErrorResponseModel errorResponseModel = null;
            try
            {
                var firmModel = _firmdetailService.GetById(Convert.ToInt32(FirmId), ref errorResponseModel);

                if (firmModel != null)
                {
                    return Ok(firmModel);
                }

                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpPost]
        //[Authorize]
        [ProducesResponseType(typeof(FirmdetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddFirm([FromForm]FirmdetailModel model)
        {

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.EnteredBy = Convert.ToInt32(userId);
            }
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var firmdetailModel = _firmdetailService.AddFirm(model, ref errorMessage);
                if (!string.IsNullOrEmpty(firmdetailModel))
                {
                    return Ok(firmdetailModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpPatch()]
        //[Authorize]
        [ProducesResponseType(typeof(FirmdetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Put([FromForm]FirmdetailModel model)
        {

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.ChangedBy = Convert.ToInt32(userId);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var firmModel = _firmdetailService.Put(model, ref errorMessage);
                if (firmModel)
                {
                    return Ok(firmModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }

        }
        [HttpDelete]
        //[Authorize]
        [Route("Delete")]
        [ProducesResponseType(typeof(FirmdetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Delete(long FirmId )
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var firmmodel = _firmdetailService.Delete(FirmId, ref errorResponseModel);

                if (firmmodel != null)
                {
                    return Ok(firmmodel);
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
