using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
   public interface ICropNotificationService
    {
        List<CropNotificationModel> GetAllNotification();
        string AddCropNotification(CropNotificationModel model, ref ErrorResponseModel errorResponseModel);
        CropNotificationModel GetNotificationById(long NotificationId, ref ErrorResponseModel errorResponseModel);
        public bool PutNotification(CropNotificationModel model, ref ErrorResponseModel errorResponseModel);
        string DeleteNotification(long NotificationId, ref ErrorResponseModel errorResponseModel);
        List<NotificationPhaseModel> GetAll();
        string Add(NotificationPhaseModel model, ref ErrorResponseModel errorResponseModel);
        NotificationPhaseModel GetById(long NotificationPhaseId, ref ErrorResponseModel errorResponseModel);
        public bool Put(NotificationPhaseModel model, ref ErrorResponseModel errorResponseModel);
        string Delete(long NotificationPhaseId, ref ErrorResponseModel errorResponseModel);

    }
}
