using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using System;
using System.Net;
using System.Net.Mail;

namespace FM.Portal.FrameWork.Email
{
    public class EmailService : IEmailService
    {
        private ViewEmail infoMail;
        private readonly IEmailLogsService _logsService;
        public EmailService(IEmailLogsService logsService)
        {
            _logsService = logsService;
        }

        public Result<EmailStatusType> SendMail(string To, string Subject, string TextMessage, Guid UserID)
        {
            infoMail = new ViewEmail()
            {
                EmailAddress="mirshekarWeb@gmail.com",
                Password="farhadweb",
                Title="فروشگاه اینترنتی"
            };
            return SendMail(infoMail, To, Subject, TextMessage, SQLHelper.CheckGuidNull("405CE603-2494-44B1-B3CA-4FEA82E95976"));
        }

        private Result<EmailStatusType> SendMail(ViewEmail From , string To, string Subject, string TextMessage, Guid UserID)
        {
            if (string.IsNullOrWhiteSpace(From.Title))
            {
                From.Title = "فروشگاه اینترنتی";
            }

            //string body = renderEmailBody(textMessage);
            var status = EmailStatusType.تحویل_داده_شده;

            try
            {
                var message = new MailMessage();

                message.From = new MailAddress(From.EmailAddress, From.Title, System.Text.UTF8Encoding.UTF8);
                message.To.Add(new MailAddress(To));
                message.Subject = Subject;
                message.Body = TextMessage;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = From.EmailAddress.Trim(),
                        Password = From.Password.Trim()
                    };

                    smtp.Credentials = new NetworkCredential(From.EmailAddress.Trim(), From.Password.Trim());
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = false;
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                status = EmailStatusType.ناموفق;
            }

            var log = new EmailLogs
            {
                From = From.EmailAddress,
                To = To,
                Message = TextMessage,
                IP = Helper.GetIP(),
                UserID = UserID,
                EmailStatusType = status,
            };

            //_logsService.Add(log);

            return Result<EmailStatusType>.Successful(data: status);
        }
    }
}
