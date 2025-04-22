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
    public class MachinaryProductMasterService : IMachinaryProductMasterService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MachinaryProductMasterService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }


        public List<MachinaryProductMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var MachinaryProductMasterModelList = new List<MachinaryProductMasterModel>();
            var MachinaryProductMasterModelListEntity = (from MachinaryProductMaster in _agriContext.MachinaryProductMasters

                                                         join MachinaryBrandMaster in _agriContext.MachinaryBrandMasters
                                                         on MachinaryProductMaster.MachinaryBrandId equals MachinaryBrandMaster.MachinaryBrandId

                                                         join MachinaryCategoryMaster in _agriContext.MachinaryCategoryMasters
                                                         on MachinaryProductMaster.MachinaryCategoryId equals MachinaryCategoryMaster.MachinaryCategoryId
                                                         where MachinaryProductMaster.DeleteStatus==false

                                                         select new
                                                         {
                                                             MachinaryProductMaster.MachinaryProductId,
                                                             MachinaryProductMaster.MachinaryProductName,
                                                             MachinaryProductMaster.MachinaryBrandId,
                                                             MachinaryProductMaster.MachinaryCategoryId,
                                                             MachinaryProductMaster.ModelName,
                                                             MachinaryProductMaster.SequenceNo,
                                                             MachinaryProductMaster.ActualPrice,
                                                             MachinaryProductMaster.DiscountPrice,
                                                             MachinaryProductMaster.EnteredBy,
                                                             MachinaryProductMaster.EnteredDate,
                                                             MachinaryProductMaster.ChangedBy,
                                                             MachinaryProductMaster.ChangedDate,
                                                             MachinaryProductMaster.DeleteStatus,
                                                             MachinaryBrandMaster.MachinaryBrandName,
                                                             MachinaryCategoryMaster.MachinaryCategoryName,

                                                         }
                                  ).ToList();
            if (MachinaryProductMasterModelListEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in MachinaryProductMasterModelListEntity)
            {
                var model = new MachinaryProductMasterModel();
                model.MachinaryProductId = item.MachinaryProductId;
                model.MachinaryProductName = item.MachinaryProductName;
                model.MachinaryBrandId = item.MachinaryBrandId;
                model.MachinaryBrandName = item.MachinaryBrandName;
                model.MachinaryCategoryId = item.MachinaryCategoryId;
                model.MachinaryCategoryName = item.MachinaryCategoryName;
                model.ActualPrice = item.ActualPrice;
                model.ModelName = item.ModelName;
                model.DiscountPrice = item.DiscountPrice;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                model.SequenceNo = item.SequenceNo;

                MachinaryProductMasterModelList.Add(model);
            }
            return MachinaryProductMasterModelList;
        }



        MachinaryProductMasterModel IMachinaryProductMasterService.GetById(long MachinaryProductId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var MachinaryProductMasterEntity = (from MachinaryProductMaster in _agriContext.MachinaryProductMasters

                                                join MachinaryBrandMaster in _agriContext.MachinaryBrandMasters
                                                on MachinaryProductMaster.MachinaryBrandId equals MachinaryBrandMaster.MachinaryBrandId

                                                join MachinaryCategoryMaster in _agriContext.MachinaryCategoryMasters
                                                on MachinaryProductMaster.MachinaryCategoryId equals MachinaryCategoryMaster.MachinaryCategoryId

                                                where MachinaryProductMaster.MachinaryProductId == MachinaryProductId && MachinaryProductMaster.DeleteStatus==false
                                                select new
                                                {
                                                    MachinaryProductMaster.MachinaryProductId,
                                                    MachinaryProductMaster.MachinaryProductName,
                                                    MachinaryProductMaster.MachinaryBrandId,
                                                    MachinaryProductMaster.MachinaryCategoryId,
                                                    MachinaryProductMaster.ModelName,
                                                    MachinaryProductMaster.SequenceNo,
                                                    MachinaryProductMaster.ActualPrice,
                                                    MachinaryProductMaster.DiscountPrice,
                                                    MachinaryProductMaster.EnteredBy,
                                                    MachinaryProductMaster.EnteredDate,
                                                    MachinaryProductMaster.ChangedBy,
                                                    MachinaryProductMaster.ChangedDate,
                                                    MachinaryProductMaster.DeleteStatus,
                                                    MachinaryBrandMaster.MachinaryBrandName,
                                                    MachinaryCategoryMaster.MachinaryCategoryName,

                                                }
                                  ).FirstOrDefault();
            if (MachinaryProductMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new MachinaryProductMasterModel
            {
                MachinaryProductId = MachinaryProductMasterEntity.MachinaryProductId,
                MachinaryProductName = MachinaryProductMasterEntity.MachinaryProductName,
                MachinaryBrandId = MachinaryProductMasterEntity.MachinaryBrandId,
                MachinaryCategoryId = MachinaryProductMasterEntity.MachinaryCategoryId,
                MachinaryBrandName = MachinaryProductMasterEntity.MachinaryBrandName,
                MachinaryCategoryName = MachinaryProductMasterEntity.MachinaryCategoryName,
                ModelName = MachinaryProductMasterEntity.ModelName,
                ActualPrice = MachinaryProductMasterEntity.ActualPrice,
                DiscountPrice = MachinaryProductMasterEntity.DiscountPrice,
                EnteredBy = MachinaryProductMasterEntity.EnteredBy,
                EnteredDate = MachinaryProductMasterEntity.EnteredDate,
                ChangedBy = MachinaryProductMasterEntity.ChangedBy,
                ChangedDate = MachinaryProductMasterEntity.ChangedDate,
                DeleteStatus = MachinaryProductMasterEntity.DeleteStatus,
                SequenceNo = MachinaryProductMasterEntity.SequenceNo,
            };

        }

        public string Add(MachinaryProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            {

                var message = string.Empty;

                var existing = _agriContext.MachinaryProductMasters.Any(x => x.SequenceNo == model.SequenceNo);
                if (existing)
                {
                    return null;
                }
                else
                {
                    var machinaryProductMasterEntity = new MachinaryProductMaster();
                    machinaryProductMasterEntity.MachinaryProductId = model.MachinaryProductId;
                    machinaryProductMasterEntity.MachinaryBrandId = model.MachinaryBrandId;
                    machinaryProductMasterEntity.MachinaryProductName = model.MachinaryProductName;
                    machinaryProductMasterEntity.MachinaryCategoryId = model.MachinaryCategoryId;
                    machinaryProductMasterEntity.ModelName = model.ModelName;
                    machinaryProductMasterEntity.ActualPrice = model.ActualPrice;
                    machinaryProductMasterEntity.DiscountPrice = model.DiscountPrice;
                    machinaryProductMasterEntity.EnteredBy = model.EnteredBy;
                    machinaryProductMasterEntity.EnteredDate = DateTime.Now;
                    machinaryProductMasterEntity.DeleteStatus = model.DeleteStatus;
                    machinaryProductMasterEntity.SequenceNo = model.SequenceNo;
                    _agriContext.MachinaryProductMasters.Add(machinaryProductMasterEntity);
                    _agriContext.SaveChanges();

                    message = "MachinaryProductMaster Added Succesfully ";
                }
                return message;
            }

        }
        public bool Put(MachinaryProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {

            var MachinaryProductId = Convert.ToInt32(model.MachinaryProductId);
            var MachinaryProductMasterEntity = _agriContext.MachinaryProductMasters.FirstOrDefault(x => x.MachinaryProductId == MachinaryProductId && !x.DeleteStatus);
            if (MachinaryProductMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                MachinaryProductMasterEntity.MachinaryProductId = model.MachinaryProductId;
                MachinaryProductMasterEntity.MachinaryBrandId = model.MachinaryBrandId;
                MachinaryProductMasterEntity.MachinaryProductName = model.MachinaryProductName;
                MachinaryProductMasterEntity.MachinaryCategoryId = model.MachinaryCategoryId;
                MachinaryProductMasterEntity.ModelName = model.ModelName;
                MachinaryProductMasterEntity.ActualPrice = model.ActualPrice;
                MachinaryProductMasterEntity.DiscountPrice = model.DiscountPrice;
                MachinaryProductMasterEntity.SequenceNo = model.SequenceNo;
                MachinaryProductMasterEntity.ChangedBy = model.ChangedBy;
                MachinaryProductMasterEntity.ChangedDate = DateTime.Now;
                MachinaryProductMasterEntity.DeleteStatus = model.DeleteStatus;

                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long MachinaryProductId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            MachinaryProductMasterModel model = new MachinaryProductMasterModel();
            var machinaryProductMasterEntity = _agriContext.MachinaryProductMasters.FirstOrDefault(x => x.MachinaryProductId == MachinaryProductId);
            if (machinaryProductMasterEntity != null)
            {
                machinaryProductMasterEntity.DeleteStatus = true;
                machinaryProductMasterEntity.ChangedBy = model.ChangedBy;
                machinaryProductMasterEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


    }
}


