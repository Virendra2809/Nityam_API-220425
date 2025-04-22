using HealthIndex.Model;

namespace HealthIndex.Business.Interface
{
    public interface ILoginService
    {
        public bool LoginUser(LoginModel model, ref ErrorResponseModel errorResponseModel);
    }
}
