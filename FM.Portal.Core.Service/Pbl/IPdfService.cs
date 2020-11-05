using System;

namespace FM.Portal.Core.Service
{
   public interface IPdfService:IService
    {
        Result<string> PrintPaymentToPdf(Guid PaymentID,Guid LanguageID);
    }
}
