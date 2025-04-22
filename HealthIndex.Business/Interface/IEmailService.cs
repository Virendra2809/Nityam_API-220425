using HealthIndex.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IEmailService
    {
       
        bool SendMail(EmailSenderModel emailSenderModel);
    }
}
