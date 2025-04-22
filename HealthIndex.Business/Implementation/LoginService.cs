using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net;

namespace HealthIndex.Business.Implementation
{
    public class LoginService : ILoginService
    {

        HealthIndexDbContext _HealthIndexDbContext;
        IConfiguration _configuration;
        public LoginService(HealthIndexDbContext HealthIndexDbContext, IConfiguration configuration)
        {
            _HealthIndexDbContext = HealthIndexDbContext;
            _configuration = configuration;
           
        }

        public bool LoginUser(LoginModel loginmodel, ref ErrorResponseModel errorResponseModel)
        {
            var userEntity = _HealthIndexDbContext.UserMasters.FirstOrDefault(x => x.EmailId == loginmodel.Email && x.UserPassword.Equals(@loginmodel.Password));
            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

