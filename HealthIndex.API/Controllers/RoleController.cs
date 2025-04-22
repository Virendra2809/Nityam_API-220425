using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;
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
    public class RoleController : BaseAPIController
    {
        IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;

        }

        [HttpGet]
        // [Authorize]
        [ProducesResponseType(typeof(RoleModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllRole()
        {
            var errorMessage = new ErrorResponseModel();
            try
            {
                var model = new StateModel();
                var productModel = _roleService.GetAllRole();
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

    }
}
