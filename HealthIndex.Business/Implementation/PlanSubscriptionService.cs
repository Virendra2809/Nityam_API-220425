using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using Microsoft.Recognizers.Text.Matcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

namespace HealthIndex.Business.Implementation
{
    public class PlanSubscriptionService : IPlanSubscription
    {
        HealthIndexDbContext _healthindexdbcontext;
        public PlanSubscriptionService(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }

        public List<PlanSubscriptionModel> GetPlanSubscription(ref ErrorResponseModel errorResponseModel)
        {
            var PlanSubscriptionModelList = new List<PlanSubscriptionModel>();
            ResponseMessage responseMessage = new ResponseMessage();
            errorResponseModel = new ErrorResponseModel();
           
            var PlanSubscriptionEntityList = _healthindexdbcontext.SubscriptionPlans.ToList();

   if (PlanSubscriptionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "Feedback Details not found";
                return null;
            }
            else
            {
                PlanSubscriptionModelList = PlanSubscriptionEntityList
                    .Select(planEntity => new PlanSubscriptionModel
                    {
                        PlanId = planEntity.PlanId,
                        Code = planEntity.Code,
                        Status = planEntity.Status,
                        OfferCode = planEntity.OfferCode.TrimEnd(),
                        Amount = planEntity.Amount.TrimEnd(),
                        CreatedBy = planEntity.CreatedBy,
                        Name = planEntity.Name,
                        Validity = planEntity.Validity,
                    })
                    .ToList();
            }


            return PlanSubscriptionModelList;
        }

        public Model.Message AddEditPlanSubscription(PlanSubscriptionModel model, ref ErrorResponseModel errorResponseModel)
        {
             Model. Message messagemodel=new Model.Message();

            var planDataCount = _healthindexdbcontext.SubscriptionPlans
        .FirstOrDefault(x => x.OfferCode == model.OfferCode);



            if (model.PlanId != 0)
            {
               

                var planData = _healthindexdbcontext.SubscriptionPlans.FirstOrDefault(x => x.PlanId == model.PlanId);

                if (planDataCount.OfferCode == planData.OfferCode || model.OfferCode == "")
                {
                    var planEntity = new SubscriptionPlan();
                    planData.PlanId = model.PlanId;
                    planData.Name = model.Name;
                    planData.Code = model.Code.TrimEnd();
                    planData.Amount = model.Amount.TrimEnd();
                    planData.Status = model.Status;
                    planData.OfferCode = model.OfferCode.TrimEnd();
                    planData.ModifyBy = model.CreatedBy;
                    planData.ModifyDate = DateTime.Now;
                    planData.Validity = model.Validity;
                    _healthindexdbcontext.SaveChanges();
                    messagemodel.message = GlobalConstants.PlanUpdateSuccessfully;
                }
                else
                {
                    messagemodel.message = GlobalConstants.DuplicatePromoCode;
                }
             
            }
            else
            {
                if(planDataCount == null || model.OfferCode == "")
                {
                    var planEntity = new SubscriptionPlan();
                    planEntity.PlanId = model.PlanId;
                    planEntity.Name = model.Name;
                    planEntity.Code = GenerateAlphanumericCode(4).TrimEnd();
                    planEntity.Amount = model.Amount.TrimEnd();
                    planEntity.Status = model.Status;
                    planEntity.OfferCode = model.OfferCode.TrimEnd();
                    planEntity.CreatedBy = model.CreatedBy;
                    planEntity.CreatedDate = DateTime.Now;
                    planEntity.Validity = model.Validity;
                    _healthindexdbcontext.SubscriptionPlans.Add(planEntity);
                    _healthindexdbcontext.SaveChanges();
                    messagemodel.message = GlobalConstants.PlanSavedSuccessfully;
                }
                else
                {
                    messagemodel.message = GlobalConstants.DuplicatePromoCode;
                }
            }
            return messagemodel;
        }

        static string GenerateOfferCode(int length)
        {
            const string alphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder codeBuilder = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(alphanumericChars.Length);
                codeBuilder.Append(alphanumericChars[index]);
            }

            return codeBuilder.ToString();
        }


        static string GenerateAlphanumericCode(int length)
        {
            const string alphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder codeBuilder = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(alphanumericChars.Length);
                codeBuilder.Append(alphanumericChars[index]);
            }

            return codeBuilder.ToString();
        }

        public PlanSubscriptionModel GetPlanSubscriptionById(long planId, ref ErrorResponseModel errorResponseModel)
        {
            PlanSubscriptionModel planSubscriptionModel = new PlanSubscriptionModel();

            var PlanEntity = (from planEntity in _healthindexdbcontext.SubscriptionPlans
                              where planEntity.PlanId == planId
                              select new PlanSubscriptionModel
                              {
                                PlanId = planEntity.PlanId,
                                Code = planEntity.Code.TrimEnd(),
                                Status = planEntity.Status,
                                OfferCode = planEntity.OfferCode.TrimEnd(),
                                Amount = planEntity.Amount.TrimEnd(),
                                CreatedBy = planEntity.CreatedBy,
                                Name = planEntity.Name,
                                Validity = planEntity.Validity,
        }).FirstOrDefault();

            if (PlanEntity == null) 
            {
                errorResponseModel.StatusCode = HttpStatusCode.OK;
                errorResponseModel.Message = "Plan not found";
                return null;
            }
            else
            {
                planSubscriptionModel = PlanEntity;
            }
           
            // Map properties from planEntity to planSubscriptionModel
          

            // Add more properties as needed

            return planSubscriptionModel;
        }

    }
}
