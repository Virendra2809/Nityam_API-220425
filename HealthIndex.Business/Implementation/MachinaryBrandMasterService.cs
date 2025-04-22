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
    public class MachinaryBrandMasterService : IMachinaryBrandMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public MachinaryBrandMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public List<MachinaryBrandMasterModel> GetAllMachinaryBrandMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var machinaryBrandModelList = new List<MachinaryBrandMasterModel>();
            var machinaryBrandMasterListEntity = _agriContext.MachinaryBrandMasters.Where(x => x.DeleteStatus == false).ToList();
            if (machinaryBrandMasterListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in machinaryBrandMasterListEntity)
            {
                var model = new MachinaryBrandMasterModel();
                model.MachinaryBrandId = item.MachinaryBrandId;
                model.MachinaryBrandName = item.MachinaryBrandName;
                model.Description = item.Description;
                model.SeqNo = Convert.ToInt32(item.SeqNo);
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate =item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                machinaryBrandModelList.Add(model);
            }
            return machinaryBrandModelList;
        }

        public MachinaryBrandMasterModel GetMachinaryBrandMasterById(long MachinaryBrandId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var machinaryBrandMasterEntity = _agriContext.MachinaryBrandMasters.FirstOrDefault(x => x.MachinaryBrandId == MachinaryBrandId && !x.DeleteStatus);
            if (machinaryBrandMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new MachinaryBrandMasterModel
            {
                MachinaryBrandName = machinaryBrandMasterEntity.MachinaryBrandName,
                Description = machinaryBrandMasterEntity.Description,
                SeqNo = machinaryBrandMasterEntity.SeqNo,
                EnteredBy = machinaryBrandMasterEntity.EnteredBy,
                EnteredDate = machinaryBrandMasterEntity.EnteredDate,
                ChangedBy = machinaryBrandMasterEntity.ChangedBy,
                ChangedDate = machinaryBrandMasterEntity.ChangedDate
            };
        }

        public string AddMachinaryBrandMaster(MachinaryBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingMachinaryBrand = _agriContext.MachinaryBrandMasters.Any(x => x.SeqNo == model.SeqNo);
            if (existingMachinaryBrand)
            {
                return null;
            }
            else
            {
                MachinaryBrandMaster machinaryBrandMasterEntity = new MachinaryBrandMaster();
                machinaryBrandMasterEntity.MachinaryBrandName = model.MachinaryBrandName;
                machinaryBrandMasterEntity.Description = model.Description;
                machinaryBrandMasterEntity.SeqNo = model.SeqNo;
                machinaryBrandMasterEntity.EnteredBy = model.EnteredBy;
                machinaryBrandMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.MachinaryBrandMasters.Add(machinaryBrandMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public bool UpdateMachinaryBrandMaster(MachinaryBrandMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var machinaryBrandId = model.MachinaryBrandId;
            var machinaryBrandMasterEntity = _agriContext.MachinaryBrandMasters.FirstOrDefault(x => x.MachinaryBrandId == machinaryBrandId);
            if (machinaryBrandMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                machinaryBrandMasterEntity.MachinaryBrandName = model.MachinaryBrandName;
                machinaryBrandMasterEntity.Description = model.Description;
                machinaryBrandMasterEntity.SeqNo = model.SeqNo;
                machinaryBrandMasterEntity.ChangedBy = model.ChangedBy;
                machinaryBrandMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }

        public string DeleteMachinaryBrandMaster(long MachinaryBrandId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            MachinaryBrandMasterModel model = new MachinaryBrandMasterModel();
            var machinaryBrandMasterEntity = _agriContext.MachinaryBrandMasters.FirstOrDefault(x => x.MachinaryBrandId == MachinaryBrandId);
            if (machinaryBrandMasterEntity != null)
            {
                machinaryBrandMasterEntity.DeleteStatus = true;
                machinaryBrandMasterEntity.ChangedBy = model.ChangedBy;
                machinaryBrandMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }                
    }
}
