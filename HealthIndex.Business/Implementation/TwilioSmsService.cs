using HealthIndex.Business.Interface;
using HealthIndex.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HealthIndex.Business.Implementation
{
    public class TwilioSmsService : ITwilioSmsService
    {
        private readonly TwilioSettings _twilioSettings;
        public TwilioSmsService(IOptions<TwilioSettings> twillioSettings)
        {
            _twilioSettings = twillioSettings.Value;
        }
        public Task<string> SendSms(string mobileNumber, string messageBody, bool isWhatsapp = false)
        {
            try
            {
                var SmsResult = "";
                TwilioClient.Init(_twilioSettings.AccountID, _twilioSettings.AuthToken);
                var message = MessageResource.Create(
                    to: new PhoneNumber(isWhatsapp ? $"whatsapp:+{mobileNumber}" : mobileNumber),
                    from: new PhoneNumber(isWhatsapp ? _twilioSettings.WhatsAppNumber : _twilioSettings.PhoneNumber),
                    body: messageBody);
                Console.WriteLine(message.Sid);
                SmsResult = message.Sid;
                return Task.FromResult(SmsResult);
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

    }
}

