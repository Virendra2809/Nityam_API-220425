using StartUpX.Business.Interface;
using StartUpX.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseAPIController
    {
        IAuthService _authService;
        IConfiguration _configuration;
        public LoginController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("EmailLogin")]
        public IActionResult EmailLogin([FromForm]LoginModel model)
        {
            var authModel = new AuthModel();
            try
            {
                ErrorResponseModel errorResponseModel = null;
                if (!ModelState.IsValid)
                {
                    var errorMessage = string.Join(",", ModelState.Values.ToList());
                    return BadRequest(new { message = errorMessage });
                }
                if (!string.IsNullOrEmpty(model.Email))
                {
                    authModel = _authService.EmailLogin(model.Email, model.Password, ref errorResponseModel);


                    if (authModel != null)
                    {
                        var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, authModel.UserId.ToString()),
                        new Claim(ClaimTypes.Role, authModel.Role),
                    };

                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                        var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                        var userToken = new JwtSecurityTokenHandler().WriteToken(token);
                        var userModel = new UserResponseModel();

                        userModel.token = userToken;
                        userModel.userName = authModel.FirstName + authModel.LastName;
                        userModel.role = authModel.Role;
                        return Ok(userModel);
                    }
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
