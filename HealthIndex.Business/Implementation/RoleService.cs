using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class RoleService:IRoleService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value => throw new NotImplementedException();
        public RoleService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<RoleModel> GetAllRole()
        {
            var errorResponseModel = new ErrorResponseModel();
            var RoleList = new List<RoleModel>();
            var RoleListEntity = (from RoleMaster in _agriContext.RoleMasters
                                      where RoleMaster.DeleteStatus == false
                                      select new
                                      {
                                          RoleMaster.RoleId,
                                          RoleMaster.RoleName,
                                          RoleMaster.FirmIds,
                                          RoleMaster.EnteredBy,
                                          RoleMaster.EnteredDate,
                                          RoleMaster.ChangedBy,
                                          RoleMaster.ChangedDate,
                                          RoleMaster.DeleteStatus,
                                      }
                                  ).ToList();
            if (RoleListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in RoleListEntity)
            {
                var model = new RoleModel();
                model.RoleId = item.RoleId;
                model.RoleName = item.RoleName;
                model.FirmIds = Convert.ToInt32(item.FirmIds);
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;

                RoleList.Add(model);
            }
            return RoleList;



        }

    }
}
