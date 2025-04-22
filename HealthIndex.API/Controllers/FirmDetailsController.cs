using HealthIndex.Business.Interface;
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
    public class FirmDetailsController : BaseAPIController
    {
        IFirmDetailService _IFirmDetailService;

        public FirmDetailsController(IFirmDetailService firmdetailService)
        {
            _IFirmDetailService = firmdetailService;
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(FirmDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetFirmDetails()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var firmdetailModelList = _IFirmDetailService.GetFirmDetails(ref errorResponseModel);

                if (firmdetailModelList != null)
                {
                    return Ok(firmdetailModelList);
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
