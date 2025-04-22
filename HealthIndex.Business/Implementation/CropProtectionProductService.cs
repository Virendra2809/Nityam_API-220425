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
    public class CropProtectionProductService:ICropProtectionProductService
    {
        private AgtonomicsAgriCultureDbContext _agriContext;
        private IConfiguration _configuration;

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CropProtectionProductService(AgtonomicsAgriCultureDbContext agriContext, IConfiguration configuration)
        {
            _agriContext = agriContext;
            _configuration = configuration;
        }


        public List<CropProtectionProductMasterModel> GetAll()
        {
            var errorResponseModel = new ErrorResponseModel();
            var userExist = _agriContext.CropProtectionProductMasters.FirstOrDefault(x => x.DeleteStatus == false);

            var cropProtectionProductMasterModelList = new List<CropProtectionProductMasterModel>();
            var cropProtectionProductMasterModelEntity = (from CropProtectionProductMaster in _agriContext.CropProtectionProductMasters
                                                          join CropDiseaseMaster in _agriContext.CropDiseaseMasters
                                                           on CropProtectionProductMaster.CropDiseaseId equals CropDiseaseMaster.CropDiseaseId

                                                          join CropProtectionBrandMaster in _agriContext.CropProtectionBrandMasters
                                                          on CropProtectionProductMaster.CropProtectionBrandId equals CropProtectionBrandMaster.CropProtectionBrandId

                                                          join CropInsectMaster in _agriContext.CropInsectMasters
                                                           on CropProtectionProductMaster.CropInsectId equals CropInsectMaster.CropInsectId
                                                          where CropProtectionProductMaster.DeleteStatus == false

                                                          join UnitofMeasurementMaster in _agriContext.UnitofMeasurementMasters
                                                          on CropProtectionProductMaster.UnitId equals UnitofMeasurementMaster.UnitId
                                                          where CropProtectionProductMaster.DeleteStatus == false


                                                          select new
                                                          {
                                                              CropProtectionProductMaster.CropProtectionProductId,
                                                              CropProtectionBrandMaster.CropProtectionBrandId,
                                                              CropProtectionProductMaster.CropProtectionProductName,
                                                              CropProtectionProductMaster.Quantity,
                                                              CropProtectionProductMaster.Amount,
                                                              UnitofMeasurementMaster.UnitId,
                                                              CropProtectionProductMaster.DiscountAmount,
                                                              CropProtectionProductMaster.EnteredBy,
                                                              CropProtectionProductMaster.EnteredDate,
                                                              CropProtectionProductMaster.ChangedBy,
                                                              CropProtectionProductMaster.ChangedDate,
                                                              CropProtectionProductMaster.DeleteStatus,
                                                              CropProtectionProductMaster.SequenceNo,
                                                              CropDiseaseMaster.CropDiseaseId,
                                                              CropInsectMaster.CropInsectId,
                                                              CropDiseaseMaster.CropDiseaseName,
                                                              CropInsectMaster.CropInsectName,
                                                              CropProtectionBrandMaster.CropProtectionBrandName,
                                                              UnitofMeasurementMaster.UnitName,
                                                          }
                                  ).ToList();

            if (cropProtectionProductMasterModelEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            foreach (var item in cropProtectionProductMasterModelEntity)
            {
                var model = new CropProtectionProductMasterModel();
                model.CropProtectionProductId = item.CropProtectionProductId;
                model.CropProtectionBrandId = item.CropProtectionBrandId;
                model.CropProtectionProductName = item.CropProtectionProductName;
                model.Quantity = item.Quantity;
                model.Amount = item.Amount;
                model.UnitId = item.UnitId;
                model.DiscountAmount = item.DiscountAmount;
                model.EnteredBy = item.EnteredBy;
                model.EnteredDate = item.EnteredDate;
                model.ChangedBy = item.ChangedBy;
                model.ChangedDate = item.ChangedDate;
                model.DeleteStatus = item.DeleteStatus;
                model.SequenceNo = item.SequenceNo;
                model.CropDiseaseId = item.CropDiseaseId;
                model.CropInsectId = item.CropInsectId;
                model.CropInsectName = item.CropInsectName;
                model.CropDiseaseName = item.CropDiseaseName;
                model.CropProtectionBrandName = item.CropProtectionBrandName;
                model.UnitName = item.UnitName;

                cropProtectionProductMasterModelList.Add(model);
            }
            return cropProtectionProductMasterModelList;
        }

        public string Add(CropProtectionProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {
            {

                var message = string.Empty;

                var existing = _agriContext.CropProtectionProductMasters.Any(x => x.SequenceNo == model.SequenceNo);
                if (existing)
                {
                    return null;
                }
                else
                {
                    var CropProtectionProductMasterEntity = new CropProtectionProductMaster();

                    CropProtectionProductMasterEntity.CropProtectionProductId = model.CropProtectionProductId;
                    CropProtectionProductMasterEntity.CropProtectionBrandId = model.CropProtectionBrandId;
                    CropProtectionProductMasterEntity.CropProtectionProductName = model.CropProtectionProductName;
                    CropProtectionProductMasterEntity.Quantity = model.Quantity;
                    CropProtectionProductMasterEntity.Amount = model.Amount;
                    CropProtectionProductMasterEntity.UnitId = model.UnitId;
                    CropProtectionProductMasterEntity.DiscountAmount = model.DiscountAmount;
                    CropProtectionProductMasterEntity.EnteredBy = model.EnteredBy;
                    CropProtectionProductMasterEntity.EnteredDate = DateTime.Now;
                    CropProtectionProductMasterEntity.DeleteStatus = model.DeleteStatus;
                    CropProtectionProductMasterEntity.SequenceNo = model.SequenceNo;
                    CropProtectionProductMasterEntity.CropDiseaseId = model.CropDiseaseId;
                    CropProtectionProductMasterEntity.CropInsectId = model.CropInsectId;

                    _agriContext.CropProtectionProductMasters.Add(CropProtectionProductMasterEntity);
                    _agriContext.SaveChanges();

                    message = "  CropProtectionProductMaster Added Succesfully ";
                }
                return message;
            }

        }
        CropProtectionProductMasterModel ICropProtectionProductService.GetById(long CropProtectionProductId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var cropProtectionProductMasterEntity = (from CropProtectionProductMaster in _agriContext.CropProtectionProductMasters

                                                     join CropDiseaseMaster in _agriContext.CropDiseaseMasters
                                                      on CropProtectionProductMaster.CropDiseaseId equals CropDiseaseMaster.CropDiseaseId

                                                     join CropProtectionBrandMaster in _agriContext.CropProtectionBrandMasters
                                                     on CropProtectionProductMaster.CropProtectionBrandId equals CropProtectionBrandMaster.CropProtectionBrandId

                                                     join CropInsectMaster in _agriContext.CropInsectMasters
                                                     on CropProtectionProductMaster.CropInsectId equals CropInsectMaster.CropInsectId
                                                    // where CropProtectionProductMaster.CropProtectionProductId == CropProtectionProductId && CropProtectionProductMaster.DeleteStatus == false

                                                     join UnitofMeasurementMaster in _agriContext.UnitofMeasurementMasters
                                                     on CropProtectionProductMaster.UnitId equals UnitofMeasurementMaster.UnitId
                                                     where CropProtectionProductMaster.CropProtectionProductId == CropProtectionProductId && CropProtectionProductMaster.DeleteStatus == false

                                                     select new
                                                     {
                                                         CropProtectionProductMaster.CropProtectionProductId,
                                                         CropProtectionBrandMaster.CropProtectionBrandId,
                                                         CropProtectionProductMaster.CropProtectionProductName,
                                                         CropProtectionProductMaster.Quantity,
                                                         CropProtectionProductMaster.Amount,
                                                         UnitofMeasurementMaster.UnitId,
                                                         CropProtectionProductMaster.DiscountAmount,
                                                         CropProtectionProductMaster.EnteredBy,
                                                         CropProtectionProductMaster.EnteredDate,
                                                         CropProtectionProductMaster.ChangedBy,
                                                         CropProtectionProductMaster.ChangedDate,
                                                         CropProtectionProductMaster.DeleteStatus,
                                                         CropProtectionProductMaster.SequenceNo,
                                                         CropDiseaseMaster.CropDiseaseId,
                                                         CropInsectMaster.CropInsectId,
                                                         CropDiseaseMaster.CropDiseaseName,
                                                         CropInsectMaster.CropInsectName,
                                                         CropProtectionBrandMaster.CropProtectionBrandName,
                                                         UnitofMeasurementMaster.UnitName,
                                                     }
                                  ).FirstOrDefault();
            if (cropProtectionProductMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return null;
            }
            return new CropProtectionProductMasterModel
            {
                CropProtectionProductId = cropProtectionProductMasterEntity.CropProtectionProductId,
                CropProtectionBrandId = cropProtectionProductMasterEntity.CropProtectionBrandId,
                CropProtectionProductName = cropProtectionProductMasterEntity.CropProtectionProductName,
                UnitId = cropProtectionProductMasterEntity.UnitId,
                Amount = cropProtectionProductMasterEntity.Amount,
                DiscountAmount = cropProtectionProductMasterEntity.DiscountAmount,
                Quantity = cropProtectionProductMasterEntity.Quantity,
                EnteredBy = cropProtectionProductMasterEntity.EnteredBy,
                EnteredDate = cropProtectionProductMasterEntity.EnteredDate,
                ChangedBy = cropProtectionProductMasterEntity.ChangedBy,
                ChangedDate = cropProtectionProductMasterEntity.ChangedDate,
                DeleteStatus = cropProtectionProductMasterEntity.DeleteStatus,
                SequenceNo = cropProtectionProductMasterEntity.SequenceNo,
                CropDiseaseId = cropProtectionProductMasterEntity.CropDiseaseId,
                CropInsectId = cropProtectionProductMasterEntity.CropInsectId,
                CropDiseaseName = cropProtectionProductMasterEntity.CropDiseaseName,
                CropInsectName = cropProtectionProductMasterEntity.CropInsectName,
                CropProtectionBrandName = cropProtectionProductMasterEntity.CropProtectionBrandName,
                UnitName = cropProtectionProductMasterEntity.UnitName,
            };

        }


        public bool Put(CropProtectionProductMasterModel model, ref ErrorResponseModel errorResponseModel)
        {

            var CropProtectionProductId = Convert.ToInt32(model.CropProtectionProductId);
            var CropProtectionProductMasterEntity = _agriContext.CropProtectionProductMasters.FirstOrDefault(x => x.CropProtectionProductId == CropProtectionProductId && !x.DeleteStatus);
            if (CropProtectionProductMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return false;
            }
            else
            {
                CropProtectionProductMasterEntity.CropProtectionProductId = model.CropProtectionProductId;
                CropProtectionProductMasterEntity.CropProtectionBrandId = model.CropProtectionBrandId;
                CropProtectionProductMasterEntity.CropProtectionProductName = model.CropProtectionProductName;
                CropProtectionProductMasterEntity.Quantity = model.Quantity;
                CropProtectionProductMasterEntity.Amount = model.Amount;
                CropProtectionProductMasterEntity.UnitId = model.UnitId;
                CropProtectionProductMasterEntity.DiscountAmount = model.DiscountAmount;
                CropProtectionProductMasterEntity.ChangedBy = model.ChangedBy;
                CropProtectionProductMasterEntity.ChangedDate = DateTime.Now;
                CropProtectionProductMasterEntity.DeleteStatus = model.DeleteStatus;
                CropProtectionProductMasterEntity.SequenceNo = model.SequenceNo;
                CropProtectionProductMasterEntity.CropDiseaseId = model.CropDiseaseId;
                CropProtectionProductMasterEntity.CropInsectId = model.CropInsectId;


                _agriContext.SaveChanges();
                return true;
            }
        }

        public string Delete(long CropProtectionProductId, ref ErrorResponseModel errorResponseModel)
        {
            var message = "";
            errorResponseModel = new ErrorResponseModel();
            CropProtectionProductMasterModel model = new CropProtectionProductMasterModel();
            var cropProtectionProductEntity = _agriContext.CropProtectionProductMasters.FirstOrDefault(x => x.CropProtectionProductId == CropProtectionProductId);
            if (cropProtectionProductEntity != null)
            {
                cropProtectionProductEntity.DeleteStatus = true;
                cropProtectionProductEntity.ChangedBy = model.ChangedBy;
                cropProtectionProductEntity.ChangedDate = DateTime.Now;
                _agriContext.SaveChanges();
                message = "Deleted Successfully";
            }
            return message;
        }


    }



}

