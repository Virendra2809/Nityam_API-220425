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
    public class ModuleMasterService : IModuleMasterServices
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ModuleMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }

        public List<ModuleMasterModel> GetAllModulemaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var ModuleMasterModelList = new List<ModuleMasterModel>();
            var ModuleListEntity = (from ModuleMaster in _agriContext.ModuleMasters
                                    where ModuleMaster.DeleteStatus==false

                                  select new
                                  {
                                      
                                      ModuleMaster.ModuleId,
                                      ModuleMaster.ModuleName,
                                      ModuleMaster.ModuleMarathiName,
                                      ModuleMaster.ModuleIcon,
                                      ModuleMaster.ModuleAreaName,
                                      ModuleMaster.Seqno,
                                      ModuleMaster.IsDirectNode,
                                      ModuleMaster.ControllerName,
                                      ModuleMaster.ActionName,
                                      ModuleMaster.ModuleUrl,
                                      ModuleMaster.EnteredBy,
                                      ModuleMaster.EnteredDate,
                                      ModuleMaster.ChangedBy,
                                      ModuleMaster.ChangedDate,
                                      ModuleMaster.DeleteStatus,

                                  }
                                  ).ToList();
            if (ModuleListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in ModuleListEntity)
            {
                var model = new ModuleMasterModel();
                model.ModuleId = item.ModuleId;
                model.ModuleName = item.ModuleName;
                model.ModuleMarathiName = item.ModuleMarathiName;
                model.ModuleIcon = item.ModuleIcon;
                model.ModuleIcon = item.ModuleIcon;
                model.Seqno = item.Seqno;
                model.ModuleAreaName = item.ModuleAreaName;
                model.IsDirectNode = item.IsDirectNode;
                model.ControllerName = item.ControllerName;
                model.ActionName = item.ActionName;
                model.ModuleUrl = item.ModuleUrl;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;

                ModuleMasterModelList.Add(model);
            }
            return ModuleMasterModelList;

        

    }

        ModuleMasterModel IModuleMasterServices.GetById(long ModuleId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var ModuleMasterEntity = _agriContext.ModuleMasters.FirstOrDefault(x => x.ModuleId == ModuleId && !x.DeleteStatus);
            if (ModuleMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new ModuleMasterModel
            {
                 ModuleId=ModuleMasterEntity.ModuleId,
                 ModuleName= ModuleMasterEntity.ModuleName,
                 ModuleMarathiName=ModuleMasterEntity.ModuleMarathiName,
                 ModuleIcon=ModuleMasterEntity.ModuleIcon,
                 ModuleAreaName= ModuleMasterEntity.ModuleAreaName,
                 Seqno= ModuleMasterEntity.Seqno,
                 IsDirectNode= ModuleMasterEntity.IsDirectNode,
                 ControllerName= ModuleMasterEntity.ControllerName,
                 ActionName=ModuleMasterEntity.ActionName,
                 ModuleUrl= ModuleMasterEntity.ModuleUrl,
                 EnteredBy= ModuleMasterEntity.EnteredBy,
                 EnteredDate= ModuleMasterEntity.EnteredDate,
                  ChangedBy= ModuleMasterEntity.ChangedBy,
                  ChangedDate= ModuleMasterEntity.ChangedDate,
                  DeleteStatus= ModuleMasterEntity.DeleteStatus,


            };

        }
        public string AddModule(ModuleMasterModel model, ref ErrorResponseModel errorResponseModel)
        {

            var message = string.Empty;

            var existing = _agriContext.ModuleMasters.Any(x => x.ModuleId == model.ModuleId);
            if (existing)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                var ModuleMasterEntity = new ModuleMaster();

                ModuleMasterEntity.ModuleName = model.ModuleName;
                ModuleMasterEntity.ModuleMarathiName = model.ModuleMarathiName;
                ModuleMasterEntity.ModuleIcon = model.ModuleIcon;
                ModuleMasterEntity.ModuleAreaName = model.ModuleAreaName;
                ModuleMasterEntity.Seqno = model.Seqno;
                ModuleMasterEntity.IsDirectNode = model.IsDirectNode;
                ModuleMasterEntity.ControllerName = model.ControllerName;
                ModuleMasterEntity.ActionName = model.ActionName;
                ModuleMasterEntity.ModuleUrl = model.ModuleUrl;
                ModuleMasterEntity.EnteredBy = model.EnteredBy;
                ModuleMasterEntity.EnteredDate = DateTime.Now;
                ModuleMasterEntity.DeleteStatus = model.DeleteStatus; 
                _agriContext.ModuleMasters.Add(ModuleMasterEntity);
                _agriContext.SaveChanges();
                message = "Module Added  Successfully";

            }
            return message;
        }

        public bool Put(ModuleMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var ModuleId = Convert.ToInt32(model.ModuleId);
            var ModuleEntity = _agriContext.ModuleMasters.FirstOrDefault(x => x.ModuleId == ModuleId);
            if (ModuleEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {

                
                ModuleEntity.ModuleName = model.ModuleName;
                ModuleEntity.ModuleMarathiName = model.ModuleMarathiName;
                ModuleEntity.ModuleIcon = model.ModuleIcon;
                ModuleEntity.ModuleAreaName = model.ModuleAreaName;
                ModuleEntity.Seqno = model.Seqno;
                ModuleEntity.IsDirectNode = model.IsDirectNode;
                ModuleEntity.ControllerName = model.ControllerName;
                ModuleEntity.ActionName = model.ActionName;
                ModuleEntity.ModuleUrl = model.ModuleUrl;
                ModuleEntity.ChangedBy = model.ChangedBy;
                ModuleEntity.ChangedDate = DateTime.Now;
                ModuleEntity.DeleteStatus = model.DeleteStatus;

                _agriContext.SaveChanges();
                return true;
            }
        }
        public string DeleteModule(long ModuleId, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            errorResponseModel = new ErrorResponseModel();
            ModuleMasterModel model = new ModuleMasterModel();
            var modulemasterEntity = _agriContext.ModuleMasters.FirstOrDefault(x => x.ModuleId == model.ModuleId);
            if (modulemasterEntity != null)
            {
                modulemasterEntity.DeleteStatus = true;
                modulemasterEntity.ChangedBy = model.ChangedBy;
                modulemasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                Message = "Data  Deleted Successfully";
            }
            return Message;
        }

        

    }
}
