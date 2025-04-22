using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IUserService
    {
        List<UserModel> GetAllUser();
        string AddUser(UserModel model, ref ErrorResponseModel errorResponseModel);
        UserModel GetUserById(long userId, ref ErrorResponseModel errorResponseModel);
        public bool ActivateUser(UserModel model, ref ErrorResponseModel errorResponseModel);
    }
}
