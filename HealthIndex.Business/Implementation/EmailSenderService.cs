using HealthIndex.Business.Interface;
using HealthIndex.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HealthIndex.Business.Implementation
{
    public class EmailSenderService : IEmailSenderService
    {
        private ConfigurationsModel _configuration;


        public EmailSenderService(IOptions<EmailSettings> emailSettings, IOptions<ConfigurationsModel> hostName)
        {
            _emailSettings = emailSettings.Value;
            this._configuration = hostName.Value;
        }

        public EmailSettings _emailSettings { get; }
        public EmailSettings _emailSettings1 { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            SendEmail(email, subject, message);
            return Task.FromResult(0);
        }

        private void SendEmail(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailAsync(string email, string subject, string message, Dictionary<string, MemoryStream> attachments)
        {
            await SendEmail(email, subject, message, attachments);
        }

        private Task SendEmail(string email, string subject, string message, Dictionary<string, MemoryStream> attachments)
        {
            throw new NotImplementedException();
        }

        public Task SendSmsAsync(string phonenumber, string subject, string message)
        {
            using (var web = new System.Net.WebClient())
            {
                try
                {
                    string url = "http://103.233.79.217/api/mt/SendSMS?user=MeshBA&password=Mahesh@123&senderid=MESHBA&channel=Trans&DCS=0&flashsms=0&number="
                        + phonenumber
                        + "&text= SMSBell-Rx: You have received a SMS, from device: Akshaya Agri with Sub: " + subject
                        + message + " MeshBA&route=8&peid=1701159146303386050&dlttemplateid=1707162090147685530";

                    string result = web.DownloadString(url);
                    
                }
                catch (Exception ex)
                {
                    
                }
            }
            return Task.FromResult(0);
        }

       
        public async Task Execute(string email, string subject, string message)
        {
            try
            { 
                string toEmail = string.IsNullOrEmpty(email)
                                 ? _emailSettings.ToEmail
                                 : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Health Index")
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                try
                {
                    using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                    {
                        try
                        {
                            smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                            smtp.EnableSsl = _emailSettings.EnableSsl;
                            smtp.UseDefaultCredentials = _emailSettings.UseDefaultCredentials;                          
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            await smtp.SendMailAsync(mail);
                        }
                        catch (SmtpException ex)
                        {
                            try
                            {

                                using (StreamWriter w = File.AppendText("log.txt"))
                                {
                                    Log(ex.Message, w);
                                    Log(ex.InnerException.Message, w);
                                }

                                using (StreamReader r = File.OpenText("log.txt"))
                                {
                                    DumpLog(r);
                                }
                            }
                            catch (Exception ex1)
                            {
                                Console.WriteLine(ex.Message);

                            }


                            await Task.FromResult(ex.Message);
                        }
                    }
                }
                catch (SmtpException ex)
                {
                    await Task.FromResult(ex.Message);
                }


            }
            catch (SmtpException ex)
            {               

                await Task.FromResult(ex.Message);
               
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
        public async Task Execute(string email, string subject, string message, Dictionary<string, MemoryStream> attachments)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email)
                                 ? _emailSettings.ToEmail
                                 : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "AakshayaAgri")
                };
                mail.To.Add(new MailAddress(toEmail));
                try
                {
                    mail.CC.Add(new MailAddress(_emailSettings.CcEmail));
                }
                catch { }
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                foreach (var item in attachments)
                {
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                    mail.Attachments.Add(new Attachment(item.Value, item.Key, ct.MediaType));
                }
                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }

            }
            catch (Exception ex)
            {
                //do something here
            }
        }

        private void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
           
            var currentFolder = Directory.GetCurrentDirectory();

            using (StreamWriter writer = new StreamWriter(currentFolder, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        
    }
}
