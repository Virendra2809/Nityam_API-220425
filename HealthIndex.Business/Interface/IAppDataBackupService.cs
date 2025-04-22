using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IAppDataBackupService
    {
        ResponseMessage AddAppDataBackup(AppDataBackupModel model, ref ErrorResponseModel errorResponseModel);
        string AppDataRestore(int appUserId, ref ErrorResponseModel errorResponseModel);
        string GetFilePath(int? appUserId);

        string GetFileName(int? appUserId);


    }
}
