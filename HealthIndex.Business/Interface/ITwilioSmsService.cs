using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface ITwilioSmsService
    {
        public Task<string> SendSms(string mobileNumber, string messageBody, bool isWhatsapp = false);
    }
}
