using FM.Portal.BaseModel;
using System;

namespace FM.Portal.Core.Model
{
    public class BaseDocument<T>:Entity where T : struct, IConvertible 
    {
        public DocumentType Type { get; set; }
        public Guid RemoverID { get; set; }
        public DateTime RemoverDate { get; set; }
    }
}
