using System;

namespace FM.Portal.Core.Model
{
    public class Sales: Sales<SalesDocState> { }
    public class Sales<T>:BaseDocument<T> where T : struct, IConvertible
    {
        public Guid PaymentID { get; set; }
    }
}
