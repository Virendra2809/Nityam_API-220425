using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IUserModelService
    {
        string ForgotPasswordLink(string email, ref ErrorResponseModel errorResponseModel);

        public List<AppUserListModel> GetRegisteredUser();
        AdminUserModel GetAdminUserDetailsById(long userId, ref ErrorResponseModel errorResponseModel);
        string EditAdminUsers(AdminUserModel model, ref ErrorResponseModel errorResponseModel);
        UserCountModel GetCount(ref ErrorResponseModel errorResponseModel);

        

    }
}
