using System;

namespace FM.Portal.Core.Model
{
    public class Sales: Sales<SalesDocState> { }
    public class Sales<T>:BaseDocument<T> where T : struct, IConvertible
    {
        public Guid PaymentID { get; set; }
        public DocumentType Type { get; set; }

        //only show
        public string BuyerInfo { get; set; }
        public string BuyerPhone { get; set; }
        public decimal Price { get; set; }
        public string TransactionStatusMessage { get; set; }
        public int Total { get; set; }
    }
}
