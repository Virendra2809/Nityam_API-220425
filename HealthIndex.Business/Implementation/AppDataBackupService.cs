using HealthIndex.Business.Interface;
using HealthIndex.Common;
using HealthIndex.Entity.DataModels;
using HealthIndex.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Twilio.Base;
using System.Web;
using System.IO;
using System.Drawing;
using Twilio.TwiML.Messaging;
using static System.Net.Mime.MediaTypeNames;
using Twilio.Http;
using System.Text.Json;

namespace HealthIndex.Business.Implementation
{
    public class AppDataBackupService : IAppDataBackupService
    {
        HealthIndexDbContext _healthindexdbcontext;
        public AppDataBackupService(HealthIndexDbContext centrumContext)
        {
            _healthindexdbcontext = centrumContext;
        }
       
        public ResponseMessage AddAppDataBackup(AppDataBackupModel model, ref ErrorResponseModel errorResponseModel)
        {
            ResponseMessage response = new ResponseMessage(); 
            var message = string.Empty;
            var appdataEntityExists = _healthindexdbcontext.AppDataBackups.Where(x => x.AppUserId == model.AppUserId).ToList();
            if (appdataEntityExists != null)
            {
                foreach (var item in appdataEntityExists)
                {
                    item.IsActive = false;
                    _healthindexdbcontext.SaveChanges();
                    //message = "App Data Backup Update Successfully";
                }
            }
            AppDataBackup appdataEntity = new AppDataBackup();
            // appdataEntity.DataBackupId = model.DataBackupId;
            appdataEntity.AppUserId = (int)model.AppUserId;
            appdataEntity.BackupDate = DateTime.Now;
            appdataEntity.BackupPath = model.BackupPath;
            appdataEntity.IsActive = true;
            appdataEntity.IsRestored = false;
            appdataEntity.RestoreDate = null;
            _healthindexdbcontext.AppDataBackups.Add(appdataEntity);
            _healthindexdbcontext.SaveChanges();
            response.message = "App Data Backup Save Successfully";
            return response;

        }

        public string GetFilePath(int? appUserId)
        {
            var dataEntity = _healthindexdbcontext.AppDataBackups.FirstOrDefault(x => x.AppUserId == appUserId && x.IsActive == true);
            if (dataEntity != null)
            {
                return dataEntity.BackupPath;
            }
            return null;
        }
        public  string AppDataRestore(int appUserId, ref ErrorResponseModel errorResponseModel)
        {
            string message="";
            var dataEntity = _healthindexdbcontext.AppDataBackups.FirstOrDefault(x => x.AppUserId == appUserId && x.IsActive == true);
            
            if (dataEntity != null)
            {
                var filePath = dataEntity.BackupPath;
                if (System.IO.File.Exists(filePath))
                {
                    {
                        dataEntity.IsRestored = true;
                        dataEntity.RestoreDate = DateTime.Now;
                        _healthindexdbcontext.SaveChanges();
                        message = "App Data Backup Restore Successfully";
                    }

                }
            }
            return message;
        }




        public string GetFileName(int? appUserId)
        {
            var latestDataEntity = _healthindexdbcontext.AppDataBackups
                .Where(x => x.AppUserId == appUserId && x.IsActive == true)
                .FirstOrDefault();

            if (latestDataEntity != null)
            {
                string backupPath = latestDataEntity.BackupPath;
                string[] pathSegments = backupPath.Split('/');
                string lastSegment = pathSegments[pathSegments.Length - 1];  // Get the last segment after splitting by '/'
                int startIndex = lastSegment.IndexOf('_') + 1;
                int endIndex = lastSegment.LastIndexOf('.');

                if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
                {
                    string fileNamePart = lastSegment.Substring(startIndex, endIndex - startIndex);
                    var resultObject = new
                    {
                        filename = fileNamePart
                    };
                    return JsonSerializer.Serialize(resultObject);
                }
            }
            return null;
        }



    }
}
