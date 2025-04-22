using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace HealthIndex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : BaseAPIController
    {
        IAuthService _authService;
        IConfiguration _configuration;
        private readonly string filePath;

        public Login(IAuthService authService, IConfiguration configuration, string filePath)
        {
            _authService = authService;
            _configuration = configuration;
            this.filePath = filePath;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        
        public IActionResult Authenticate(LoginModel model)
        {
            try
            {
                ErrorResponseModel errorResponseModel = null;
                if (!ModelState.IsValid)
                {
                    var errorMessage = string.Join(",", ModelState.Values.ToList());
                    return BadRequest(new { message = errorMessage });
                }
                var authData = _authService.AuthenticateUser(model.Email, model.Password, ref errorResponseModel);
                if (authData != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, authData.UserId.ToString()),
                        new Claim(ClaimTypes.Role, authData.Role),
                    };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        authData.Role,
                        authData.UserName,
                        authData.UserId,
                        authData.EmailId,
                    });
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
