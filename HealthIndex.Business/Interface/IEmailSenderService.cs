using HealthIndex.Model;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HealthIndex.Business.Interface
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailAsync(string email, string subject, string message, Dictionary<string, MemoryStream> attachments);
        Task SendSmsAsync(string phonenumber, string subject, string message);
        Task Execute(string email, string subject, string message);

        
    }
}
