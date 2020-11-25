using System;
using System.IO;

namespace FM.Portal.Core.Service
{
   public interface IPdfService:IService
    {
        Result PrintPaymentToPdf(Stream stream,Guid PaymentID,Guid LanguageID);
    }
}
