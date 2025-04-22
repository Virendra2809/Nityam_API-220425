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
    public class MachinaryCategoryMasterService : IMachinaryCategoryMasterService
    {
        AgtonomicsAgriCultureDbContext _agriContext;
        IConfiguration _configuration;

        public MachinaryCategoryMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;

        }

        public string AddMachinaryCategoryMaster(MachinaryCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingMachinaryCategory = _agriContext.MachinaryCategoryMasters.Any(x => x.SeqNo == model.SeqNo);
            if (existingMachinaryCategory)
            {
                return null;
            }
            else
            {
                MachinaryCategoryMaster machinaryCategoryMasterEntity = new MachinaryCategoryMaster();
                machinaryCategoryMasterEntity.MachinaryCategoryName = model.MachinaryCategoryName;
                machinaryCategoryMasterEntity.Description = model.Description;
                machinaryCategoryMasterEntity.SeqNo = model.SeqNo;
                machinaryCategoryMasterEntity.EnteredBy = model.EnteredBy;
                machinaryCategoryMasterEntity.EnteredDate = DateTime.Now;
                _agriContext.MachinaryCategoryMasters.Add(machinaryCategoryMasterEntity);
                _agriContext.SaveChanges();
                message = "Added Successfully";
            }
            return message;
        }

        public string DeleteMachinaryCategoryMaster(long MachinaryCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            MachinaryCategoryMasterModel model = new MachinaryCategoryMasterModel();
            var machinaryCategoryMasterEntity = _agriContext.MachinaryCategoryMasters.FirstOrDefault(x => x.MachinaryCategoryId == MachinaryCategoryId);
            if (machinaryCategoryMasterEntity != null)
            {
                machinaryCategoryMasterEntity.DeleteStatus = true;
                machinaryCategoryMasterEntity.ChangedBy = model.ChangedBy;
                machinaryCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }

        public List<MachinaryCategoryMasterModel> GetAllMachinaryCategoryMaster()
        {
            var errorResponseModel = new ErrorResponseModel();
            var machinaryCategoryModelList = new List<MachinaryCategoryMasterModel>();
            var machinaryCategoryMasterListEntity = _agriContext.MachinaryCategoryMasters.Where(x => x.DeleteStatus == false).ToList();
            if (machinaryCategoryMasterListEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in machinaryCategoryMasterListEntity)
            {
                var model = new MachinaryCategoryMasterModel();
                model.MachinaryCategoryId = item.MachinaryCategoryId;
                model.MachinaryCategoryName = item.MachinaryCategoryName;
                model.Description = item.Description;
                model.SeqNo = Convert.ToInt32(item.SeqNo);
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                machinaryCategoryModelList.Add(model);
            }
            return machinaryCategoryModelList;
        }

        public MachinaryCategoryMasterModel GetMachinaryCategoryMasterById(long MachinaryCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var machinaryCategoryMasterEntity = _agriContext.MachinaryCategoryMasters.FirstOrDefault(x => x.MachinaryCategoryId == MachinaryCategoryId && !x.DeleteStatus);
            if (machinaryCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new MachinaryCategoryMasterModel
            {
                MachinaryCategoryId = machinaryCategoryMasterEntity.MachinaryCategoryId,
                MachinaryCategoryName = machinaryCategoryMasterEntity.MachinaryCategoryName,
                Description = machinaryCategoryMasterEntity.Description,
                SeqNo = (int)machinaryCategoryMasterEntity.SeqNo,
                EnteredBy = machinaryCategoryMasterEntity.EnteredBy,
                EnteredDate = machinaryCategoryMasterEntity.EnteredDate,
                ChangedBy = machinaryCategoryMasterEntity.ChangedBy,
                ChangedDate = machinaryCategoryMasterEntity.ChangedDate
            };
        }

        public bool UpdateMachinaryCategoryMaster(MachinaryCategoryMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            var machinaryCategoryId = model.MachinaryCategoryId;
            var machinaryCategoryMasterEntity = _agriContext.MachinaryCategoryMasters.FirstOrDefault(x => x.MachinaryCategoryId == machinaryCategoryId);
            if (machinaryCategoryMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                machinaryCategoryMasterEntity.MachinaryCategoryName = model.MachinaryCategoryName;
                machinaryCategoryMasterEntity.Description = model.Description;
                machinaryCategoryMasterEntity.SeqNo = model.SeqNo;
                machinaryCategoryMasterEntity.ChangedBy = model.ChangedBy;
                machinaryCategoryMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                return true;
            }
        }
    }
}
