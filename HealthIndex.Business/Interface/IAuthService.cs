using HealthIndex.Entity.DataModels;
using HealthIndex.Model;

namespace HealthIndex.Business.Interface
{
    public interface IAuthService
    {
      

        AuthorModel AuthenticateUser(string email, string password, ref ErrorResponseModel errorResponseModel);
        string CreateNewOTP(AppUserMaster model,  string otp);
       
    }
}
