using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using System;

namespace FM.Portal.FrameWork.Email
{
   public interface IEmailService
    {
        Result<EmailStatusType> SendMail(string To, string Subject, string TextMessage, Guid UserID);
    }
}
